using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchPeoManageWeb.Models
{
    public class MBuilding:BasicModel
    {
        /// <summary>
        /// 建筑标识ID
        /// </summary>
        [Key]
        [Column("building_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuildingID {  get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        [Column("building_name")]
        public string BuildingName {  get; set; }

        /// <summary>
        /// 最低楼层
        /// </summary>
        [Column("min_floor")]
        public int MinFloor {  get; set; }

        /// <summary>
        /// 最高楼层
        /// </summary>
        [Column("max_floor")]
        public int MaxFloor { get; set; }

        /// <summary>
        /// 建筑拼音助记码
        /// </summary>
        [Column("pym")]
        public string PYM {  get; set; }

        /// <summary>
        /// 建筑下属教室
        /// </summary>
        [IgnoreForInsert]
       public List<MBuilding> Classrooms { get; set; }
        /// <summary>
        /// 建筑下属教室
        /// </summary>
        [IgnoreForInsert]
        public string TreeName { get; set; }

    }
}
