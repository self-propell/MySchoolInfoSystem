using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MMajor:BasicModel
    {
        /// <summary>
        /// 专业唯一标识
        /// </summary>
        [Column("major_id")]
        public int MajorID {  get; set; }

        ///
        [Column("major_name")]
        public string MajorName { get; set; }

        /// <summary>
        /// 所属学院ID
        /// </summary>
        [Column("school_id")]
        public int SchoolID { get; set; }

    }
}
