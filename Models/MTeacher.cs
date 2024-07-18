using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MTeacher:BasicModel
    {
        /// <summary>
        /// 教师唯一标识，主键
        /// </summary>
        [Column("teacher_id")]
        public int TeacherId { get; set; }

        /// <summary>
        /// 教师职工号，唯一
        /// </summary>
        [Column("employee_id")]
        public int? EmployeeID { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 教师性别
        /// </summary>
        [Column("sex")]
        public string? Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Column("id_number")]
        public string? IdNumber { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Column("age")]
        public string? Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 联系邮箱地址
        /// </summary>
        [Column("email")]
        public string? Email { get; set; }

        /// <summary>
        /// 就职日期【创建条目填写】
        /// </summary>
        [Column("enrollment_date")]
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// 合同到期日期
        /// </summary>
        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 职称【教授/副教授等】
        /// </summary>
        [Column("job_title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// 职位【院长/副院长等】
        /// </summary>
        [Column("job_position")]
        public string JobPosition { get; set; }

        /// <summary>
        /// 所属学院ID
        /// </summary>
        [Column("school_id")]
        public int SchoolId { get; set; }

        /// <summary>
        /// 所属学院名称【join school table】
        /// </summary>
        [Column("school_name")]
        public string? SchoolName { get; set; }

        /// <summary>
        /// 是否有编制
        /// </summary>
        [Column("is_budgeted_posts")]
        public bool IsBudgetedPosts { get; set; }

        /// <summary>
        /// 是否已离职
        /// </summary>
        [Column("is_departed")]
        public bool IsDeparted { get; set; }

        /// <summary>
        /// 离职日期【离职时填写】
        /// </summary>
        [Column("departed_date")]
        public DateTime? DepartedDate { get; set; }

        /// <summary>
        /// 离职原因【离职时填写】
        /// </summary>
        [Column("departed_reason")]
        public string? DepartedReason { get; set; }




    }
}
