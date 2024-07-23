using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    /// <summary>
    /// 课程实体
    /// </summary>
    public class MCourse:BasicModel
    {
        /// <summary>
        /// 课程数据库唯一标识
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("course_id")]
        public int CourseID {  get; set; }

        /// <summary>
        /// 课程业务用课程号
        /// </summary>
        [Column("course_pmid")]
        public string CoursePMID { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [Column("course_name")]
        public string CourseName { get; set; }

        /// <summary>
        /// 课程开设学年学期
        /// </summary>
        [Column("open_semester")]
        public string OpenSemester { get; set; }

        /// <summary>
        /// 课时
        /// </summary>
        [Column("class_hour")]
        public int ClassHour { get; set; }

        /// <summary>
        /// 课程性质
        /// </summary>
        [Column("course_nature")]
        public string CourseNature { get; set; }

        /// <summary>
        /// 所属专业ID
        /// </summary>
        [Column("major_id")]
        public int MajorID { get; set; }

        /// <summary>
        /// 所属专业名
        /// </summary>
        [Column("major_name")]
        [IgnoreForInsert]
        public string MajorName { get; set; }

    }
}
