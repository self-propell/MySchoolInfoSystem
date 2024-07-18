using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    /// <summary>
    /// 基础类操作信息
    /// </summary>
    public abstract class BasicModel
    {
        /// <summary>
        /// 本次操作是否成功
        /// </summary>
        /// <returns>bool 1:</returns>
        public bool isFailed { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        public DateTime _reqTime;
        public DateTime ReqTime 
        { 
            get { return _reqTime; }
            set { this._reqTime = DateTime.Now; } 
        }
        public string DataModel { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }

        /// <summary>
        /// 创建人【创建条目时填写】
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建条目时间【创建条目时填写】
        /// </summary>
        [Column("create_timestamp")]
        public DateTime CreateTimestamp { get; set; }

        /// <summary>
        /// 上次修改信息经手人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        [Column("update_timestamp")]
        public DateTime UpdateTimestamp { get; set; }

        /// <summary>
        /// 条目是否已注销删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 条目删除操作人
        /// </summary>
        [Column("delete_by")]
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 条目删除时间
        /// </summary>
        [Column("delete_timestamp")]
        public DateTime? DeleteTimestamp { get; set; }

        /// <summary>
        /// 条目删除原因
        /// </summary>
        [Column("delete_reason")]
        public string? DeleteReason { get; set; }
    }
}
