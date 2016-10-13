using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    // A Stupid Change
    class Program
    {
        static void Main(string[] args)
        {
            string pwd = System.Environment.GetEnvironmentVariable("MSOPWD", EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(pwd))
            {
                System.Console.WriteLine("MSOPWD user environment variable is empty, cannot continue. Press any ket yo Exit...");
                System.Console.ReadKey();
                return;
            }

            // get access to source site
            using (var ctx = new ClientContext("https://shaunn.sharepoint.com/sites/dev"))
            {
                //provide count and pwd for connecting to the source
                var passWord = new SecureString();
                foreach (char c in pwd.ToCharArray()) passWord.AppendChar(c);
                ctx.Credentials = new SharePointOnlineCredentials("snichols@shaunn.onmicrosoft.com", passWord);

                // do your thang

                ctx.Web.CreateContentType("PNP Test", "0x0101003E501AEC2DA14E75A8363337B48DADCA", "PnP Group");

                var list = ctx.Web.CreateList(ListTemplateType.DocumentLibrary, "PNP Docs", true, enableContentTypes: true);

                list.AddContentTypeToListByName("PNP Test", true);
            }  
        }
    }
}
