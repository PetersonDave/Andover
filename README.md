<h1>Andover</h1>

Andover is a .NET framework for monitoring and examining Sitecore implementations against documented best practices.

Andover consists of Categories from which statistics are gathered and summarized. Categories contain one or more components, each mapping to a measurable configuration setting within Sitecore.

<h2>How To Use</h2>
Follow the steps below to integrate into a Sitecore installation:

<ol>
	<li>Install NuGet Package: <a href="https://www.nuget.org/packages/Andover/">https://www.nuget.org/packages/Andover/</a></li>
	<li>Modify any config settings specific to your Sitecore install withi App_Config\Include\Andover.config</li>
	<li>Browse \tools\analysis.aspx</li>
</ol>

<h2>Architecture</h2>

<h3>Solution Structure</h3>

Andover consists of the following projects:

<ul>
	<li><strong>Andover.Controller</strong> - Ties everything together through references to both Andover.Domain and Andover.Data.</li>
	<li><strong>Andover.Domain</strong> - Rules from which analysis is conducted.</li>
	<li><strong>Andover.Data</strong> - Data providers used by Andover.Domain to run analysis against.</li>
</ul>

<h3>Project Structure</h3>

![Andover Architecture](https://raw.github.com/PetersonDave/Andover/master/Documentation/Architecture/architecture-high-level.jpg)

<h3>Andover.Controller</h3>

Andover.Controller is the glue for the solution. Its main responsibilities are:

<ul>
<li>Gathers settings from the main Analyzer page and distributes across projects.</li>
<li>Instantiates Providers and injects them into Components within Andover.Domain.</li>
<li>Categories are instantiated and linked to Components, also within Andover.Domain</li>
</ul>

<h3>Andover.Domain: Isolating Dependencies</h3>

Andover.Domain contains all the rules from which analysis is conducted. The Domain Model design pattern is used here to eliminate any and all dependencies. Looking at the project references, notice how it's lightweight for library dependencies:

![Andover.Domain Dependencies](https://raw.github.com/PetersonDave/Andover/master/Documentation/Architecture/Andover.Domain-dependencies.jpg)

Interfaces provide shells to code against, while at run time, Andover.Controller injects dependencies into the framework. Looking at the overall solution structure, notice how dependencies exist for all projects except for Andover.Domain (highlighted in yellow):

![Andover Dependencies](https://raw.github.com/PetersonDave/Andover/master/Documentation/Architecture/outside-dependencies.jpg)

<h2>How to Contribute</h2>

The example below outlines how to add a component, component result, and data provider.

<h3>Step 1: Create a Provider</h3>

In Andover.Domain, Create an interface for your provider 

```c#
namespace Andover.Domain.Components.Cache.Provider
{
	public interface ICacheProvider
	{
		// Returns data analysis results
		IEnumerable<CacheDelta> GetAllSitecoreCaches(int deltaThresholdPercentage);
	}
}
```

In Andover.Data, create your provider, implementing your interface above and deriving from ProviderBase. Your class will resemble the following:
	
```c#	
namespace Andover.Data.Cache.Provider
{
    public class CacheProvider : ProviderBase, ICacheProvider
    {
        public override void DisposeProvider()
        {
            // dispose provider logic
        }

        public IEnumerable<CacheDelta> GetAllSitecoreCaches(int deltaThresholdPercentage)
        {
            // pull source of component analysis here
        }
    }
}
```

<h3>Step 2: Create a Component</h3>

Create your component class, implementing IComponent and deriving from ComponentBase.
```c#	
namespace Andover.Domain.Components.Cache
{
    public class CacheComponent : ComponentBase, IComponent
    {
        private ICacheProvider _provider;

        public CacheComponent(ICacheProvider provider)
        {
            _provider = provider;
        }

        public override IEnumerable<IComponentResult> Analyze()
        {
            // perform analysis on data in provider
        }

        public bool IsCompliant
        {
            get
            {
                // logic to determine if component is compliant
            }
        }
    }
}
```
<h3>Step 3: Create Component Result</h3>

Create your results, deriving from ComponentResultBase. Providing both member attributes ComponentResultName and ComponentResultDescription ensures results are properly labeled within the report.
```c#	
namespace Andover.Domain.Components.Cache.Results
{
	public class CacheDelta : ComponentResultBase
	{
		[ComponentResultName("Size"),
		ComponentResultDescription("Cache delta size")]
		public long Size { get; set; }

		[ComponentResultName("Maximum Size"),
		ComponentResultDescription("Maximum size of cache delta")]
		public long MaxSize { get; set; }

		[ComponentResultName("Remaining Space"),
		ComponentResultDescription("Remaining space before reaching threshold")]
		public long RemainingSpace { get; set; }

		[ComponentResultName("Beyond Threshold"),
		 ComponentResultDescription("Determines if cache delta is beyond threshold")]
		public bool IsDeltaPastThreshold { get; set; }

        [ComponentResultName("Cache Name"),
         ComponentResultDescription("Cache Setting Name")]
	    public string CacheName { get; set; }
	}
}
```

<h3>Step 4: Create Configuration Settings</h3>

Update Andover.Config within Andover.Domain for any settings required:

```xml
<category name="Cache Settings" description="Cache setting analysis" >
	<components>
		<component type="Andover.Domain.Components.Cache.CacheDeltasComponent" name="Cache Deltas" description="Cache delta analysis" />
	</components>
</category>
```

<h3>Step 5: Wire Up The Provider</h3>

In Andover.Controller, update ProviderModule.cs to bind and map your new provider

```c#	
namespace Andover.Controller
{
	public class ProviderModule : NinjectModule
	{
		// append provider wire-up here
		public override void Load()
		{
			Bind<ICacheProvider>()
				.To<CacheProvider>();
		
		...
```

When complete, your folder structure should match the following:

![Andover Architecture](https://raw.github.com/PetersonDave/Andover/master/Documentation/Architecture/folder-structure.jpg)

<h2>Example Compliance Report Output</h2>

![Andover Architecture](https://raw.github.com/PetersonDave/Andover/master/Documentation/Architecture/example-report.jpg)

<h2>Sitecore Settings To Monitor</h2>

<ul>
	<li>Sitecore cache thresholds & deltas</li>
	<li>Sitecore logs
		<ul>
		<li>Application start-up frequency
			<ul>
			<li>Soft crashes</li>
			<li>Hard crashes</li>
			</ul>
		</li>
		<li>Application Error counts</li>
		<li>Long running requests frequency</li>
		</ul>
	</li>
	<li>Configuration settings
	<li>Content tree structure
		<ul>
		<li>Number of versions per item
		<li>Number of items per parent
	</ul>
	<li>Sitecore database health
		<ul>
		<li>Artifacts Cleanup
			<ul>
			<li>History table
			<li>PublishQueue table
			<li>EventQueue table
			</ul>
		<li>Database index fragmentation
		<li>Database consistency errors
		</ul>
	<li>Sitecore DMS configuration
		<ul>
		<li>Database index fragmentation
		<li>Robot traffic
		<li>Max Queue Size
		</ul>
</ul>

<h2>Terminology</h2>

<ul>
<li><strong>Andover</strong> - Reporting and analysis of Sitecore vital statistics.</li>
<li><strong>Category</strong> - Collection of common Sitecore settings or components.</li>
<li><strong>Component</strong> - A single Sitecore setting to monitor.</li>
<li><strong>Andover Compliance Score</strong> - Best Practice Compliance Score.</li>
</ul>