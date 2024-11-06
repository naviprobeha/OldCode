using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebModelDimValueCollection : CollectionBase
    {
        public WebModelDimValue this[int index]
        {
            get { return (WebModelDimValue)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebModelDimValue webModelDimValue)
        {
            return (List.Add(webModelDimValue));
        }
        public int IndexOf(WebModelDimValue webModelDimValue)
        {
            return (List.IndexOf(webModelDimValue));
        }
        public void Insert(int index, WebModelDimValue webModelDimValue)
        {
            List.Insert(index, webModelDimValue);
        }
        public void Remove(WebModelDimValue webModelDimValue)
        {
            List.Remove(webModelDimValue);
        }
        public bool Contains(WebModelDimValue webModelDimValue)
        {
            return (List.Contains(webModelDimValue));
        }

        public WebModelDimValueCollection clone()
        {
            WebModelDimValueCollection collection = new WebModelDimValueCollection();
            int i = 0;
            while (i < Count)
            {
                WebModelDimValue webModelDimValue = this[i].clone();
                collection.Add(webModelDimValue);

                i++;
            }

            return collection;
        }

        public void setSubLevelValues(WebModelDimValueCollection webModelDimValueCollection)
        {
            int i = 0;

            while (i < Count)
            {
                this[i].subLevelValues = webModelDimValueCollection;

                i++;
            }

        }

        public void bindItems(WebModel webModel)
        {
            int i = 0;

            while (i < Count)
            {
                WebModelDimValue webModelVertDim = this[i];

                int j = 0;
                while (j < webModelVertDim.subLevelValues.Count)
                {
                    WebModelDimValue webModelHorizDim = webModelVertDim.subLevelValues[j];

                    webModel.setDimension(webModel.variantDimension1Code, webModelVertDim.code);
                    webModel.setDimension(webModel.variantDimension2Code, webModelHorizDim.code);
                    WebModelVariant webModelVariant = webModel.getVariant();
                    if (webModelVariant != null)
                    {
                        webModelHorizDim.itemNo = webModelVariant.itemNo;
                        webModelHorizDim.itemVariantCode = webModelVariant.itemVariantCode;
                    }

                    webModelVertDim.subLevelValues[j] = webModelHorizDim;

                    j++;
                }

                this[i] = webModelVertDim;
                i++;
            }


        }
    }
}
