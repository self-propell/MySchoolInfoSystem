using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MSchool:BasicModel
    {
        /// <summary>
        /// 学院唯一标识
        /// </summary>
        [Column("school_id")]
        public int SchoolId {  get; set; }

        /// <summary>
        /// 学院名
        /// </summary>
        [Column("school_name")]
        public string SchoolName { get; set; }
        /// <summary>
        /// 学院描述信息【可选】
        /// </summary>
        [Column("school_description")]
        public string? SchoolDescription {  get; set; }

    }
}
