using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MTeachingClass:BasicModel
    {
        /// <summary>
        /// 教学班级唯一标识
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("class_id")]
        public int ClassID { get; set; }

        /// <summary>
        /// 教学班级业务用标识
        /// </summary>
        [Column("class_pmid")]
        public string ClassPMID { get; set; }

        /// <summary>
        /// 教学班级名称
        /// </summary>
        [Column("class_name")]
        public string ClassName { get; set; }

        /// <summary>
        /// 教师ID
        /// </summary>
        [Column("teacher_id")]
        public string TeacherID { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [Column("teacher_name")]
        [IgnoreForInsert]
        public string TeacherName { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        [Column("course_id")]
        public string CourseID { get; set; }

        /// <summary>
        /// 课程名
        /// </summary>
        [Column("course_name")]
        [IgnoreForInsert]
        public string CourseName { get; set; }

        /// <summary>
        /// 限选人数【最大人数】
        /// </summary>
        [Column("max_student_num")]
        public int MaxStuNum { get; set; }

        /// <summary>
        /// 当前选课人数
        /// </summary>
        [Column("cur_student_num")]
        public int CurStuNum { get; set; }

        /// <summary>
        /// 上课地点
        /// </summary>
        [Column("location")]
        public string Location { get; set; }
    }
}
