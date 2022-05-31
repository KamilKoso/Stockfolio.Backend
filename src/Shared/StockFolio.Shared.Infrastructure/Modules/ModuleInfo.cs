using System.Collections.Generic;

namespace Stockfolio.Shared.Infrastructure.Modules;

internal record ModuleInfo(string Name, IEnumerable<string> Policies);