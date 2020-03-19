using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Models
{

    public class MenuHelperModel
    {
        [Key]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        [NotMapped]
        public string Con_Act_Name
        {
            get
            {
                return ControllerName + "_" + ActionName;
            }
        }

        public virtual MenuModel MenuModel { get; set; }

    }

    public class MenuModel
    {
        [Key]
        public int Id { get; set; }
       
        public int MenuHelperModelId { get; set; }
       
        public  string RollId { get; set; }

        [NotMapped]
        public string MenuHelperModelIdText { get; set; }

        public string RollIdText { get; set; }

        public virtual ICollection<MenuHelperModel> MenuHelperModel { get; set; }

        public virtual MenuModelManage MenuModelManage { get; set; }


    }


    public class MenuModelManage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("MenuModel")]  
        public int MenuModelId { get; set; }

        [NotMapped]
        public string Con_Act_Roll { get; set; }
        public bool Retrive { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public virtual MenuModel MenuModel { get; set; }

       


    }


    public class StudentModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
    }


    public class DropDownListValue
    {

        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ViewDropDown
    {
        public string DropdownName { get; set; }
        public List<DropDownListValue> DropDownListValues { get; set; }
    }
}
