using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ToneVault.Web.Areas.Identity.Data.ToneVault;

// Add profile data for application users by adding properties to the WebUser class
public class WebUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

