using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewKnowledgeManager.Domain.Enums
{
    public enum Role
    {
        // Has access to manage Tenants
        Master = 10,
        // Has complete access to KnowledgeManager categories and services
        Administrator = 20,
        // Has limited access to KnowledgeManager categories and services
        Manager = 30,
        // Has minimal access to KnowledgeManager categories and services
        Editor = 40,
        //Access only the ChatterClient
        Chatter = 50,
        //Access only to history
        Auditor = 100
    }
}
