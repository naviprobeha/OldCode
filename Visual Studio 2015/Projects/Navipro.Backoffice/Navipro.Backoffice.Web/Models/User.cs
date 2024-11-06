using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;

namespace Navipro.Backoffice.Web.Models
{
    public class User
    {
        public User()
        {
            

        }

        public User(SqlDataReader dataReader)
        {
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            email = dataReader.GetValue(0).ToString();
            password = dataReader.GetValue(1).ToString();
            name = dataReader.GetValue(2).ToString();
            if (dataReader.GetValue(3).ToString() == "1") blocked = true;
            customerNo = dataReader.GetValue(4).ToString();
            defaultJobNo = dataReader.GetValue(5).ToString();
            profileCode = dataReader.GetValue(6).ToString();

            phoneNo = dataReader.GetValue(7).ToString();
            cellPhoneNo = dataReader.GetValue(8).ToString();

            resourceNo = dataReader.GetValue(9).ToString();
            sipAddress = dataReader.GetValue(10).ToString();

        }

        [Required]
        public String email { get; set; }

        [Required]
        public String password { get; set; }

        public String name { get; set; }

        public String customerNo { get; set; }

        public String defaultJobNo { get; set; }

        public String profileCode { get; set; }
        public String phoneNo { get; set; }
        public String cellPhoneNo { get; set; }
        public String sipAddress { get; set; }

        public String resourceNo { get; set; }

        public Boolean blocked { get; set; }

        public string customerName { get; set; }
        public string firstName { get { return name.Split(' ')[0]; } }
        public virtual ICollection<Role> roles { get; set; }

        public static User getUserByEmailAndPassword(Database database, string email, string password)
        {
            if (email == "") return null;
            if (password == "") return null;

            DatabaseQuery query = database.prepare("SELECT [E-mail], [Password], [Name], [Blocked], [Customer No_], [Default Job No_], [Profile Code], [Phone No_], [Cell Phone No_], [Resource No_], [SIP Address] FROM [" + database.getTableName("Case Member") + "] WITH (NOLOCK) WHERE [E-mail] = @email AND [Password] = @password");
            query.addStringParameter("email", email, 100);
            query.addStringParameter("password", md5Hash(password), 50);

            User user = null;

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {   
                user = new User(dataReader);
            }
            dataReader.Close();

            if (user != null) user.refreshCustomer(database);

            return user;
        }

        public static User getUserByEmail(Database database, string email)
        {
            DatabaseQuery query = database.prepare("SELECT [E-mail], [Password], [Name], [Blocked], [Customer No_], [Default Job No_], [Profile Code], [Phone No_], [Cell Phone No_], [Resource No_], [SIP Address] FROM [" + database.getTableName("Case Member") + "] WITH (NOLOCK) WHERE [E-mail] = @email");
            query.addStringParameter("email", email, 100);

            SqlDataReader dataReader = query.executeQuery();
            User user = null;

            if (dataReader.Read())
            {
                user = new User(dataReader);
            }
            dataReader.Close();

            return user;


        }

        public static Dictionary<string,User> getUsersWithSipAddress(Database database)
        {
            Dictionary<string, User> userTable = new Dictionary<string, User>();

            DatabaseQuery query = database.prepare("SELECT [E-mail], [Password], [Name], [Blocked], [Customer No_], [Default Job No_], [Profile Code], [Phone No_], [Cell Phone No_], [Resource No_], [SIP Address] FROM [" + database.getTableName("Case Member") + "] WITH (NOLOCK) WHERE [SIP Address] <> ''");
            
            SqlDataReader dataReader = query.executeQuery();          

            if (dataReader.Read())
            {
                User user = new User(dataReader);
                userTable.Add(user.sipAddress, user);
            }
            dataReader.Close();

            return userTable;


        }

        private static string md5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(passwordBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {

                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public void refreshCustomer(Database database)
        {
            Customer customer = Customer.getEntry(database, customerNo);
            customerName = customer.name;

            //throw new Exception("customer update: "+customerNo+", "+customerName);
        }
    }



}