using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace SeidorDemo.Models
{
    public class DocumentRepository : IDocumentRepository
    {
        public DocumentRepository()
        {
            
        }

        public IEnumerable<Document> GetAll()
        {
			return new Document[]
			{
                new Document
				{
                    DocumentName = "Xamarin Continuous Integration and Delivery",
                    DocumentURL = "http://file.allitebooks.com/20170510/Xamarin%20Continuous%20Integration%20and%20Delivery.pdf"
				},
				new Document
				{
					DocumentName = "Xamarin Studio for Android Programming",
					DocumentURL = "http://file.allitebooks.com/20170313/Xamarin%20Studio%20for%20Android%20Programming.pdf"
				},
				new Document
				{
					DocumentName = "Xamarin Mobile Application Development for iOS",
					DocumentURL = "http://file.allitebooks.com/20160812/Xamarin%20Mobile%20Application%20Development%20for%20iOS.pdf"
				},
				new Document
				{
					DocumentName = "Xamarin Mobile Development for Android Cookbook",
					DocumentURL = "http://file.allitebooks.com/20160104/Xamarin%20Mobile%20Development%20for%20Android%20Cookbook.pdf"
				},
				new Document
				{
					DocumentName = "Xamarin Mobile Application Development",
					DocumentURL = "http://file.allitebooks.com/20150709/Xamarin%20Mobile%20Application%20Development-%20Cross-Platform%20C-%20and%20Xamarin.Forms%20Fundamentals.pdf"
				}
			};
        }


    }
}
