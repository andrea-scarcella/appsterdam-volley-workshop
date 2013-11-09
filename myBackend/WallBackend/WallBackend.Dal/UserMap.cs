using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WallBackend.Core;

namespace WallBackend.Dal
{
	public class UserMap : ClassMapping<User>
    {
		public UserMap()
		{
			Table("[User]");

			Id(x => x.Id, m => m.Generator(Generators.Identity));
			Property(x => x.Username);
			Property(x => x.Password);
		}
    }
}
