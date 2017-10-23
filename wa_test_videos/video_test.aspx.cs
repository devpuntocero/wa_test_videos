using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace wa_test_videos
{
    public partial class video_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void PostXML(string fileName, string uri)
        {
            // Creamos un request usando una url que resibira el post. 
            WebRequest request = WebRequest.Create(uri);
            // Seteamos la propiedad Method del request a POST.
            request.Method = "POST";

            // Creamos lo que se va a enviar por el metodo POST y lo convertimos a byte array.
            string postData = this.GetTextFromXMLFile(fileName);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Seteamos el ContentType del WebRequest a xml.
            request.ContentType = "text/xml";
            // Seteamos el ContentLength del WebRequest.
            request.ContentLength = byteArray.Length;
            // Obtenemos el request stream.
            Stream dataStream = request.GetRequestStream();
            // escribimos la data en el request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Cerramos el Stream object.
            dataStream.Close();


            Page.Response.ContentType = "text/xml";
            // Read XML posted via HTTP
            StreamReader reader = new StreamReader(Page.Request.InputStream);
            String xmlData = reader.ReadToEnd();
            //aqui podemos hacer lo que se nos ocurra con xmlData, por ejemplo
            //podemos parsear el codigo xml y guardarlo en una base de datos por ejemplo

            if (xmlData != "" && Request.Form.Count == 0)
            {
                Application["xml"] = xmlData;
            }
        }

        private string GetTextFromXMLFile(string file)
        {
            StreamReader reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }


        protected void boton1_Click(object sender, EventArgs e)
        {
            PostXML(Server.MapPath("~/test.xml"), "http://localhost:5261/recive.aspx");
        }

        protected void boton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/recive.aspx");
        }
        protected void AjaxFileUpload1_UploadCompleteAll(object sender, AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs e)
        {

        }

        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string path = Server.MapPath("~/videos/") + e.FileName;
            AjaxFileUpload1.SaveAs(path);

            DataSet ds = new DataSet();
            ds.ReadXml(path);

            for (int i = 1 - 1; i <= ds.Tables.Count; i++)
            {
                DataTable firstTable = ds.Tables[i];
            }

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            GridView1.Visible = true;

            //string lineread;

            //XmlTextReader xmlreader = new XmlTextReader(path);


            //while (xmlreader.Read())
            //{
            //    switch (xmlreader.NodeType)
            //    {
            //        case XmlNodeType.Element: // The node is an element.
            //            lineread = xmlreader.Name;

            //            break;
            //        case XmlNodeType.Text: //Display the text in each element.
            //            Console.WriteLine(xmlreader.Value);
            //            break;
            //        case XmlNodeType.EndElement: //Display the end of the element.
            //            lineread = xmlreader.Name;
            //            break;
            //    }

            //}

        }

        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }// Use this function to get XML string from a dataset

        // Function to convert passed dataset to XML data
        public string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                // Load the XmlTextReader from the stream
                writer = new XmlTextWriter(stream, Encoding.Unicode);
                // Write to the file with the WriteXml method.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
    }
}