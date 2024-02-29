
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names", Justification = "Enums are specificly identified", Scope = "type", Target = "~T:GoC.WebTemplate.Components.Entities.SocialMediaSites")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Intended to match keyword", Scope = "member", Target = "~M:GoC.WebTemplate.Components.Utils.Caching.ICacheProvider`1.Get(System.String)~`0")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Intended to match keyword", Scope = "member", Target = "~M:GoC.WebTemplate.Components.Utils.Caching.ICacheProvider`1.Set(System.String,`0)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "All URLs are string and changing that would impact clients", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Lists can be assigned by clients and changing would impact clients", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Properties can be assigned by clients and changing would impact clients", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1014:Mark Assemblies with CLSCompliant", Justification = "Will be reviewed at a later time", Scope = "module")]