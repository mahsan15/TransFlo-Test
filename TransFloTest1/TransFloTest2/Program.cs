//Author: Muhammad Ahsan

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace TransFloTest2
{
    class Program
    {


        //Get ALL
        public  void Page_loadAll()
        {
            string sturltest = String.Format("https://jsonplaceholder.typicode.com/posts");
            WebRequest requestObjGet = WebRequest.Create(sturltest);
            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = null;
            responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();


            string strresulttest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();
            }

            Console.WriteLine(strresulttest + "Ahsan");
            Console.WriteLine("Status Code: " + responseObjGet.StatusCode);
            //200 (OK) - the action completed successfully.
            //204 (No Content) - the action completed successfully. The server response does not have a message body.
            //202 (Accepted) - action is likely to be successful but not yet complete.
            string path = @"C:\Users\Ahsan\Desktop\tflo\TransFloTest2\WorkItems.txt";//change path
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(strresulttest);

                }
            }

            
            //writes onto a text file
        }

        //GET One
        public void Page_GetbyId(int userId)
        {
            string sturltest = String.Format("https://jsonplaceholder.typicode.com/posts/" + userId);
            WebRequest requestObjGet = WebRequest.Create(sturltest);
            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = null;
            responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();


            string strresulttest = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();
            }

            Console.WriteLine(strresulttest);
            Console.WriteLine("Status Code: " + responseObjGet.StatusCode);
            //200 (OK) - the action completed successfully.
            //204 (No Content) - the action completed successfully. The server response does not have a message body.
            //202 (Accepted) - action is likely to be successful but not yet complete.
        }

        //POST (Create)
        public void Page_Create(string title, string body, int userID)
        {
            string sturl = String.Format("https://jsonplaceholder.typicode.com/posts");
            WebRequest requestObjPost = WebRequest.Create(sturl);
           
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";

            string postData = "" +
                "{\"title\":\"" + title + "\"," +
                 "\"body\":\""+ body + "\"," +
                 "\"userId\":\""+ userID + "\"}";

            using (var streamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
            {
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)requestObjPost.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result2 = streamReader.ReadToEnd();
                    Console.WriteLine(result2 + "Ahsan");

                }
                Console.WriteLine("Status Code: " + httpResponse.StatusCode);
                //200 (OK) - the action completed successfully.
                //204 (No Content) - the action completed successfully. The server response does not have a message body.
                //202 (Accepted) - action is likely to be successful but not yet complete.
            }


        }

        //DELETE
        public void Page_DeletebyId(int userId)
        {
            string stUrl = String.Format("https://jsonplaceholder.typicode.com/posts/" + userId);
            WebRequest requestObjDelete = WebRequest.Create(stUrl);
            requestObjDelete.Method = "DELETE";

            HttpWebResponse responseObjDelete = (HttpWebResponse)requestObjDelete.GetResponse();

            Console.WriteLine("Status Code: "+responseObjDelete.StatusCode);
            if(responseObjDelete.StatusCode == HttpStatusCode.OK ||
                responseObjDelete.StatusCode == HttpStatusCode.NoContent ||
                responseObjDelete.StatusCode == HttpStatusCode.Accepted)
            {
                Console.WriteLine("Successfully Deleted item with id " + userId);
            }
            else
            {
                Console.WriteLine("Could not Delete item with id " + userId);
            }
            //200 (OK) - the action completed successfully.
            //204 (No Content) - the action completed successfully. The server response does not have a message body.
            //202 (Accepted) - action is likely to be successful but not yet complete.
        }

        //PUT (update)
        public void Page_Update(string title, string body, int userId, int id)
        {
            string sturltest = String.Format("https://jsonplaceholder.typicode.com/posts/" + id);
            WebRequest requestObjPut = WebRequest.Create(sturltest);

            requestObjPut.Method = "PUT";
            requestObjPut.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(requestObjPut.GetRequestStream()))
            {
                string postData = "" +
                "{\"title\":\"" + title + "\"," +
                 "\"body\":\"" + body + "\"," +
                 "\"userId\":\"" + userId + "\"}";

                streamWriter.Write(postData);
            }
            var httpResponse = (HttpWebResponse)requestObjPut.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                Console.WriteLine(responseText);
                //Now the response.


            }
            Console.WriteLine("Status Code: " + httpResponse.StatusCode);
        }


        static void Main(string[] args)
        {
            Program pr = new Program();
            Console.WriteLine("---- Load ALL ----");
            pr.Page_loadAll();//Loads all WorkItem

            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("---- Create ----");
            pr.Page_Create("titleBaby", "This is the body", 88);//Creates WorkItem

            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.WriteLine("---- Load one ----");
            pr.Page_GetbyId(1);//Loads WorkItem with id 1

            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.WriteLine("---- Delete one ----");
            pr.Page_DeletebyId(1);//Deletes WorkItem with id 1

            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            Console.WriteLine("---- Update one ----");
            pr.Page_Update("newtitle!", "This is the body alright", 69, 1); //Updates WorkItem with id 1

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");



        }


    }
}
