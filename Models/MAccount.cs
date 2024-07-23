using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    /// <summary>
    /// 账号实体
    /// </summary>
    public class MAccount : BasicModel
    {
        /// <summary>
        /// 用户唯一标识，主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录账号【学号/工号】
        /// </summary>
        [Column("login_id")]
        public string LoginId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("password")]
        public string Password { get; set; }
        /// <summary>
        /// 账号冻结状态 0：正常 1：冻结
        /// </summary>
        [Column("is_freezed")]
        public bool IsFreezed { get; set; }

        /// <summary>
        /// 账号所属人姓名【连接学生/教师等表进行查询获取】
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
}
