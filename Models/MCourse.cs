using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MCourse:BasicModel
    {
        /// <summary>
        /// 专业唯一标识
        /// </summary>
        [Column("course_id")]
        public int CourseID {  get; set; }

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
        /// 所属学院ID【join major表】
        /// </summary>
        [Column("major_id")]
        public int SchoolID { get; set; }

    }
}
