using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MClassroom:BasicModel
    {
        /// <summary>
        /// 教室主键ID
        /// </summary>
        [Key]
        [Column("classroom_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassroomID { get; set; }

        /// <summary>
        /// 所属建筑的ID
        /// </summary>
        [Column("parent_building_id")]
        public int ParentBuildingID {  get; set; }

        /// <summary>
        /// 所在楼层
        /// </summary>
        [Column("floor")]
        public int Floor {  get; set; }

        /// <summary>
        /// 教室名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
}
