using Dunfoss.Data;
using Dunfoss.Models;
using Dunfoss.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Dunfoss.Controllers
{
    public class HomeController : Controller
    {
        IFileRepository fileRep = new EfFileRepository();
        ICurrentFile current = new EfCurrentFile();
        private IUserRepository userRepository;
        private ISecurityService _security;

        // GET: Home
        public HomeController()
        {
            _security = new SecurityService();
            userRepository = new EfUserRepository();
            //currentFile = new EfCurrentFile();
            //currentFile.InitializeCurrentFile(new CurrentFile());
            //User user = new Models.User();
            //user.Name = "first";
            //user.Password = "12345";
            //EfUserRepository efUserRep = new EfUserRepository();
            //efUserRep.CreateUser(user);
        }

        public PartialViewResult GetCurrentFiles()
        {
            CurrentFile cur = current.GetCurrentFile();
            string[] list = { cur.Path1.Split('/')[cur.Path1.Split('/').Length - 1], cur.Path2.Split('/')[cur.Path2.Split('/').Length - 1], cur.Path3.Split('/')[cur.Path3.Split('/').Length - 1] };
            return PartialView(list);
        }

        [HttpPost]
        public PartialViewResult RemoveFile(int id)
        {
            Models.File file = fileRep.GetFileById(id);
            System.IO.File.Delete(HostingEnvironment.ApplicationPhysicalPath + file.Path);
            fileRep.RemoveFile(id);
            IQueryable<Dunfoss.Models.File> list = fileRep.Files;
            List<Dunfoss.Models.File> list2 = new List<Dunfoss.Models.File>();
            int id1 = current.GetCurrentFile().FileId1;
            int id2 = current.GetCurrentFile().FileId2;
            int id3 = current.GetCurrentFile().FileId3;
            foreach (Models.File f in list)
            {
                if (f.Id != id1 && f.Id != id2 && f.Id != id3)
                    list2.Add(f);
            }
            return PartialView("GetFileList", list2.AsQueryable<Models.File>());
        }

        [HttpGet]
        public ActionResult Change(int id)
        {
            Models.File file =  fileRep.GetFileById(id);
            if(file.Type == 1)
            {
                current.UpdateFile1(file.Path);
                current.UpdateFileID1(id);
            }
            else if (file.Type == 2)
            {
                current.UpdateFile2(file.Path);
                current.UpdateFileID2(id);
            }
            else
            {
                current.UpdateFile3(file.Path);
                current.UpdateFileID3(id);
            }

            return View("GetNav");
        }

        public ActionResult GetFileList()
        {
            IQueryable<Dunfoss.Models.File> list = fileRep.Files;
            List<Dunfoss.Models.File> list2 = new List<Dunfoss.Models.File>();
            int id1 = current.GetCurrentFile().FileId1;
            int id2 = current.GetCurrentFile().FileId2;
            int id3 = current.GetCurrentFile().FileId3;
            foreach(Models.File file in list)
            {
                if (file.Id != id1 && file.Id != id2 && file.Id != id3)
                    list2.Add(file);
            }
            
            return View(list2.AsQueryable<Models.File>());
        }

        public HomeController(IUserRepository userRep)
        {
            userRepository = userRep;
            _security = new SecurityService();
        }
        public ActionResult Index()
        {
            return View("Index");
        }
        public PartialViewResult AuthenticateSection()
        {
            if (_security.IsAuthenticate())
            {
                return PartialView("_authenticatedSection", _security.GetCurrentUser());
            }
            else
            {
                return PartialView("_loginSection", new User());
            }
        }
        
        public ActionResult Login()
        {
            return PartialView("LogIn", new User());
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!_security.Authenticate(user.Name, user.Password))
                return RedirectToAction("Login");
            user = userRepository.GetUserByName(user.Name);
                _security.Login(user);
            return RedirectToAction("Index", "Home");
        }
        public PartialViewResult isAuth()
        {
            if (_security.IsAuthenticate())
                return PartialView("_logout", _security.GetCurrentUser());
            else
                return PartialView("_auth");
        }

        public ActionResult Logout()
        {
            _security.Logout();
            return RedirectToAction("Index", "Home");
        }
        public RedirectResult PhoneNumbersOfStaff()
        {
            return Redirect("http://ruecom-intru.danfoss.net/CCRS/rcptMain.aspx");
        }
        public ActionResult StatisticsOfGroup()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login");
            return View("StatisticsOfGroup");
        }
        public ViewResult LoadOfGroup()
        {
            return View("LoadOfGroup");
        }
        public ViewResult StatusCRM()
        {
            return View("StatusCRM");
        }
        public ViewResult Contacts()
        {
            return View("Contacts");
        }
    }
}