using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Navipro.Backoffice.Web.Models;
using System.Net;
using System.IO;
using Navipro.Backoffice.Web.Lib;

namespace Navipro.Backoffice.Web.Controllers
{
    public class CaseController : BaseController
    {
        // GET: Case
        public ActionResult List(string dataView = "")
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            if (dataView == "") dataView = base.User.profile.defaultDataViews["case"].code;

            DataView dataViewObject = null;

            dataViewObject = User.profile.allowedDataViews[dataView];

            ViewBag.Title = "";
            if (dataViewObject != null) ViewBag.Title = dataViewObject.name;
            ViewBag.DataView = dataView;

            ViewBag.jobList = Job.getList(database);
            ViewBag.statusList = CaseStatus.getList(database);

            return View();
        }

        // GET: /Case/View/CASE12345
        public ActionResult Details(string id)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            string caseNo = id;

            if (caseNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case caseItem = Case.getEntry(database, caseNo);
            if (caseItem == null)
            {
                return HttpNotFound();
            }

            if (base.User.user.resourceNo != "")
            {
                if ((caseItem.lastActivity == 1) && (caseItem.responsibleResource == base.User.user.resourceNo) || (caseItem.assignedResources.Contains(base.User.user.resourceNo)))
                {
                    caseItem.setLastActivity(database, 2);
                }
            }

            ViewBag.statusList = CaseStatus.getList(database);
            ViewBag.resourceList = CaseMember.getResources(database);
            ViewBag.assetSiteComments = AssetSiteComment.getList(database, caseItem.customerNo, Request.UserHostAddress);
            ViewBag.caseTagLinkList = CaseTagLink.getList(database, caseItem.no);
            ViewBag.caseTagList = CaseTag.getSelectList(database, CaseTagLink.getSelectedArray((List<CaseTagLink>)ViewBag.caseTagLinkList));
            ViewBag.caseTransactionLogList = CaseTransactionLogEntry.getList(database, caseItem.no);
            ViewBag.caseEmailReceiverList = CaseEmailRecipient.getList(database, caseItem.no);
            ViewBag.caseAttachmentList = CaseAttachment.getList(database, caseItem.no);

            return View(caseItem);
        }

 
        public ActionResult Create(string commisionType, string activityType, string status, string ordererEmail)
        {
            Case caseItem = new Models.Case();

            if ((ordererEmail != null) && (ordererEmail != ""))
            {
                CaseMember caseMember = CaseMember.getEntry(database, ordererEmail);
                if (caseMember != null)
                {
                    caseItem.ordererEmail = ordererEmail;
                    caseItem.ordererName = caseMember.name;
                    caseItem.jobNo = caseMember.defaultJobNo;
                    caseItem.customerNo = caseMember.customerNo;
                }
            }

            if (!base.authenticationCheck()) return base.getLoginView();

            ViewBag.jobList = Job.getSelectList(database);
            ViewBag.statusList = CaseStatus.getSelectList(database, status);
            ViewBag.activityTypeList = ActivityType.getSelectList(database, commisionType + "|"+activityType);
            ViewBag.resources = CaseMember.getResourceSelectList(database, new string[1], false);
            ViewBag.responsibleResources = CaseMember.getResourceSelectList(database, new string[1], true);

            return View(caseItem);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Case caseItem)
        {
            if (!base.authenticationCheck()) return base.getLoginView();
            
            try
            {
                if (Request.Form["resourceList"] != null)
                {
                    caseItem.assignedResourcesList = Request.Form["resourceList"].Split(',');
                }
                caseItem.submitCreate(database, base.User.user);
                return RedirectToAction("Details", "Case", new { id = caseItem.no });
            }
            catch(Exception e)
            {
                ViewBag.errorMessage = e.Message;

                ViewBag.jobList = Job.getSelectList(database);
                ViewBag.statusList = CaseStatus.getSelectList(database, caseItem.caseStatusCode);
                ViewBag.activityTypeList = ActivityType.getSelectList(database, caseItem.activityCommisionCode);
                ViewBag.resources = CaseMember.getResourceSelectList(database, caseItem.assignedResourcesList);
                ViewBag.responsibleResources = CaseMember.getResourceSelectList(database, new string[1] { caseItem.responsibleResource }, true);

            }
            return View();
        }

        [HttpPost]
        public ActionResult Details(Case caseItem)
        {
            if (!base.authenticationCheck()) return base.getLoginView();
            
            try
            {
                Case newCase = Case.getEntry(database, caseItem.no);
                               
                if (caseItem.mode == "createComment")
                {
                    bool includeInTimeReport = false;
                    if (Request.Form["includeInTimeReport"] == "on") includeInTimeReport = true;

                    newCase.submitCreateComment(database, base.User.user, caseItem.newComment, caseItem.caseStatusCode, includeInTimeReport);
                }
                if (caseItem.mode == "assignRespResource")
                {
                    if (Request["assignType"] == "assignMe")
                    {
                        newCase.assignResource(database, base.User.user.resourceNo);
                    }
                    else
                    {
                        newCase.assignResource(database, caseItem.responsibleResource);
                    }
                }
                if (caseItem.mode == "changeStatus")
                {
                    newCase.changeStatus(database, caseItem.caseStatusCode);
                }
                if (caseItem.mode == "deleteComment")
                {                    
                    newCase.deleteComment(database, int.Parse(Request.Form["lineNo"]));
                }
                if (caseItem.mode == "deleteCase")
                {
                    newCase.submitDelete(database, base.User.user);
                    return RedirectToAction("Index", "Home");
                }
                if (caseItem.mode == "closeCase")
                {
                    newCase.closeCase(database, caseItem.caseStatusCode);
                }
                if (caseItem.mode == "submitTags")
                {
                    //newCase.subnmitTags(database, caseItem.caseStatusCode);
                }

                return RedirectToAction("Details", "Case", new { id = caseItem.no });
            }
            catch (Exception e)
            {
                ViewBag.errorMessage = e.Message;
            }

            caseItem = Case.getEntry(database, caseItem.no);
            ViewBag.statusList = CaseStatus.getList(database);
            ViewBag.resourceList = CaseMember.getResources(database);
            ViewBag.assetSiteComments = AssetSiteComment.getList(database, caseItem.customerNo, Request.UserHostAddress);
            ViewBag.caseTagLinkList = CaseTagLink.getList(database, caseItem.no);
            ViewBag.caseTagList = CaseTag.getSelectList(database, CaseTagLink.getSelectedArray((List<CaseTagLink>)ViewBag.caseTagLinkList));
            ViewBag.caseTransactionLogList = CaseTransactionLogEntry.getList(database, caseItem.no);
            ViewBag.caseEmailReceiverList = CaseEmailRecipient.getList(database, caseItem.no);
            ViewBag.caseAttachmentList = CaseAttachment.getList(database, caseItem.no);

            return View(caseItem);
        }
        public ActionResult Edit(string id)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            string caseNo = id;

            if (caseNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case caseItem = Case.getEntry(database, caseNo);
            if (caseItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.jobList = Job.getSelectList(database);
            ViewBag.statusList = CaseStatus.getSelectList(database, caseItem.caseStatusCode);
            ViewBag.activityTypeList = ActivityType.getSelectList(database, caseItem.activityCommisionCode);
            ViewBag.resources = CaseMember.getResourceSelectList(database, caseItem.assignedResourcesList, false);
            ViewBag.responsibleResources = CaseMember.getResourceSelectList(database, new string[1] { caseItem.responsibleResource }, true);

            return View(caseItem);
        }

        public ActionResult EditDescription(string id)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            string caseNo = id;

            if (caseNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case caseItem = Case.getEntry(database, caseNo);
            if (caseItem == null)
            {
                return HttpNotFound();
            }

            return View(caseItem);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Case caseItem)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            Case dbCase = Case.getEntry(database, caseItem.no);            

            try
            {
              
               
                dbCase.subject = caseItem.subject;
                dbCase.ordererEmail = caseItem.ordererEmail;
                dbCase.ordererName = caseItem.ordererName;
                dbCase.jobNo = caseItem.jobNo;                
                dbCase.activityCommisionCode = caseItem.activityCommisionCode;
                dbCase.caseStatusCode = caseItem.caseStatusCode;
                dbCase.estimatedEndingDate = caseItem.estimatedEndingDate;
                dbCase.responsibleResource = caseItem.responsibleResource;

                dbCase.assignedResourcesList = new string[1];

                if (Request.Form["resourceList"] != null)
                {
                    dbCase.assignedResourcesList = Request.Form["resourceList"].Split(',');
                }

                

                dbCase.submitUpdate(database, base.User.user);
                
                return RedirectToAction("Details", "Case", new { id = caseItem.no });
            }
            catch (Exception e)
            {
                ViewBag.errorMessage = e.Message;

                ViewBag.jobList = Job.getSelectList(database);
                ViewBag.statusList = CaseStatus.getSelectList(database, dbCase.caseStatusCode);
                ViewBag.activityTypeList = ActivityType.getSelectList(database, dbCase.activityCommisionCode);
                ViewBag.resources = CaseMember.getResourceSelectList(database, dbCase.assignedResourcesList, false);
                ViewBag.responsibleResources = CaseMember.getResourceSelectList(database, new string[1] { dbCase.responsibleResource }, true);

            }
            return View(dbCase);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditDescription(Case caseItem)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            Case dbCase = Case.getEntry(database, caseItem.no);

            try
            {

                dbCase.description = caseItem.description;

                dbCase.submitUpdateDescription(database, base.User.user);
                return RedirectToAction("Details", "Case", new { id = caseItem.no });
            }
            catch (Exception e)
            {
                ViewBag.errorMessage = e.Message;

            }
            return View(dbCase);
        }
        public ActionResult Reply(string id)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            string caseNo = id;

            if (caseNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case caseItem = Case.getEntry(database, caseNo);
            if (caseItem == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.jobList = Job.getList(database);
            ViewBag.statusList = CaseStatus.getSelectList(database, caseItem.caseStatusCode);
            ViewBag.receiverList = CaseMember.getList(database);
 

            return View(caseItem);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Reply(Case caseItem)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            Case dbCase = Case.getEntry(database, caseItem.no);

 
            try
            {
                if (caseItem.mode == "replyToDetails")
                {
                    if (caseItem.jobNo != "")
                    {
                        dbCase.setJob(database, caseItem.jobNo);
                    }

                    ViewBag.receiverList = CaseMember.getList(database);
                    ViewBag.statusList = CaseStatus.getSelectList(database, caseItem.caseStatusCode);
                    ViewBag.jobList = Job.getList(database);

                    return View(dbCase);
                }

                string[] receiverList = Request.Form["receiverList"].Split(',');

                bool sendAsEmail = false;
                bool closeCase = false;
                if (Request.Form["sendAsEmail"] == "on") sendAsEmail = true;
                if (Request.Form["closeCase"] == "on") closeCase = true;
                
                dbCase.caseStatusCode = caseItem.caseStatusCode;
                dbCase.submitCreateReply(database, base.User.user, caseItem.newComment, receiverList, sendAsEmail, closeCase);

                if (closeCase)
                {
                    return Redirect("/");
                }
                else
                {
                    return RedirectToAction("Details", "Case", new { id = caseItem.no });
                }
            }
            catch (Exception e)
            {
                ViewBag.errorMessage = e.Message;

                ViewBag.receiverList = CaseMember.getList(database);
                ViewBag.statusList = CaseStatus.getSelectList(database, caseItem.caseStatusCode);
                ViewBag.jobList = Job.getList(database);
            }
            return View(dbCase);
        }
        public JsonResult getCaseList(string jobNoFilter, string statusFilter, string yearFilter, string dataView)
        {                      
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            if (statusFilter != "")
            {
                CaseStatus caseStatus = CaseStatus.getEntryByInt(database, int.Parse(statusFilter));
                statusFilter = caseStatus.code;
            }

            DataView dataViewObject = User.profile.allowedDataViews[dataView];

            List<Case> caseList = Case.getList(database, jobNoFilter, statusFilter, yearFilter, dataViewObject);
            return Json(caseList, JsonRequestBehavior.AllowGet);

 
        }

        public JsonResult getOrdererList()
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);


            string[] ordererList = CaseMember.getOrdererList(database);
            AutoCompleteItems autoCompleteItems = new AutoCompleteItems();
            autoCompleteItems.email = ordererList;

            return Json(autoCompleteItems, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getOrderer(string email)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            CaseMember caseMember = CaseMember.getEntry(database, email);


            return Json(caseMember, JsonRequestBehavior.AllowGet);


        }

        public ActionResult getAttachment(string caseNo, int caseLogLineNo, int entryNo)
        {
            CaseAttachment caseAttachment = CaseAttachment.getEntry(database, caseNo, caseLogLineNo, entryNo);
            if (caseAttachment == null) return null;

            Database resourcesDatabase = (Database)Session["resourcesDatabase"];

            byte[] contentArray = caseAttachment.getAttachmentContent(resourcesDatabase);
            if (contentArray == null) contentArray = caseAttachment.getAttachmentContent_old(database);
            
            
            return File(contentArray, System.Net.Mime.MediaTypeNames.Application.Octet, caseAttachment.fileName);

        }
    }
}