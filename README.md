# FSL.DatabaseImageBankInMvc

**Database Image Bank in MVC**

The goal is use a database to store images and use MVC to call those images using custom routes. The URL must be something like that: "*imagebank/sample-file*" or "*imagebank/32403404303*".


> **LIVE DEMO:**
> 
http://codefinal.com/FSL.DatabaseImageBankInMvc/

---

What is in the source code?
---

#### <i class="icon-file"></i> FSL.DatabaseImageBankInMvc

- Visual Studio solution file;
- MVC and Web API application from .NET template;
- Classes for our solution; 

> **Remarks:**

> - I created the application using the Web Application template and I have checked MVC template option. Visual Studio created a lot of files, views, scripts. I do not use them. Let's concentrate just the in our route and controller.

---

What is the goal?
---

The MVC Controller/Action will get the image by an ID "*sample-file*" or "*32403404303*" and find out on some cache and/or database and display the image. If exists in cache, get from cache if not get from database.

**Assumptions:**
- If you want do not display the image and just download the file, use that:
"*imagebank/sample-file/download*".


Source code...
---

**Controllers/ImageBankController.cs**
```csharp
public ActionResult Index(string fileId, bool download = false)
{
            var defaultImageNotFound = "pixel.gif";
            var defaultImageNotFoundPath = $"~/content/img/{defaultImageNotFound}";
            var defaultImageContentType = "image/gif";

            var cacheKey = string.Format("imagebankfile_{0}", fileId);
            Models.ImageFile model = null;

            if (Cache.NotExists(cacheKey))
            {
                model = Repository.GetFile(fileId);

                if (model == null)
                {
                    if (download)
                    {
                        return File(Server.MapPath(defaultImageNotFoundPath), defaultImageContentType, defaultImageNotFound);
                    }

                    return File(Server.MapPath(defaultImageNotFoundPath), defaultImageContentType);
                }
                
                Cache.Insert(cacheKey, "Default", model);
            }
            else
            {
                model = Cache.Get(cacheKey) as Models.ImageFile;
            }

            if (download)
            {
                return File(model.Body, model.ContentType, string.Concat(fileId, model.Extension));
            }

            return File(model.Body, model.ContentType);
}
        
public ActionResult Download(string fileId)
{
	  return Index(fileId, true);
}
```

**App_Start\RouteConfig.cs**
```csharp
public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ImageBank",
                url: GetImageBankRoute() + "/{fileId}/{action}",
                defaults: new { controller = "ImageBank", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
```


**Views/Home/Index.cshtml**
```html
<img src="~/imagebank/sample-file" />
```

----------

References:
---

- ASP.NET MVC [here][1];

Licence:
---

- [Licence MIT][4]


  [1]: https://www.asp.net/mvc
