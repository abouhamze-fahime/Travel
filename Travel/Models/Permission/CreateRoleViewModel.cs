using System.ComponentModel.DataAnnotations;

namespace Travel.Models.Permission
{
    public class CreateRoleViewModel
    {


        [Required]
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public List<int> SelectedPermission { get; set; }

    }
}
