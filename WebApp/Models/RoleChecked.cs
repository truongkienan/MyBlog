using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RoleChecked : RoleBase
    {
        public bool Checked { get; set; }
    }
}
