using System.Web.Mvc;

namespace FSL.DatabaseImageBankInMvc.Controllers
{
    public class ImageBankController : Controller
    {
        public ImageBankController()
        {
            Cache = new Cache();
            Repository = new Repository();
        }
        
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

        private Repository Repository { get; set; }

        private Cache Cache { get; set; }
    }
}