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
        [Column("name")]
        public string MajorName { get; set; }

        /// <summary>
        /// 所属学院ID
        /// </summary>
        [Column("school_id")]
        public string SchoolID { get; set; }

    }
}
