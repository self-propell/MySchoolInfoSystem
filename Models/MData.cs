using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MData:BasicModel
    {
        /// <summary>
        /// 字典类别标识
        /// </summary>
        [Column("type")]
        public int Type { get; set; }

        /// <summary>
        /// 字典数据id
        /// </summary>
        [Column("keys")]
        public int Keys { get; set; }

        /// <summary>
        /// 数据名
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [Column("mnemonic")]
        public string Mnemonic { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("is_active")]
        public bool IsActive { get; set; }



    }
}
