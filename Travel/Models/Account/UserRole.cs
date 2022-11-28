using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Models.Account
{
    [Table("tblUserRole", Schema = "cmn")]
    public class UserRole
    {
        public UserRole()
        {
        }

        [Key]
        public int Ur_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


        #region Relation  Navigation properties
        public virtual User User { get; set; } //lazy load  then use Virtual
        public virtual Role Role { get; set; }
        #endregion
    }
}
