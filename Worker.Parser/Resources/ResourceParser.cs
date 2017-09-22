using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Worker.Objects.Resources;

namespace Worker.Parser.Resources
{
    public enum ResourceType
    {
        Metal,
        Crystal,
        Deuterium
    }

    public class ResourceParser
    {
        private async Task<string> ParseResource(ResourceType type, HtmlDocument document)
        {
            switch (type)
            {
                case ResourceType.Metal:
                    return await Task.Run(() => document.GetElementbyId("metal_box").InnerText.Trim());
                case ResourceType.Crystal:
                    return await Task.Run(() => document.GetElementbyId("crystal_box").InnerText.Trim());
                case ResourceType.Deuterium:
                    return await Task.Run(() => document.GetElementbyId("deuterium_box").InnerText.Trim());
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task<Metal> GetMetal(HtmlDocument document)
        {
            var resourceAmount = await ParseResource(ResourceType.Metal, document);
            return new Metal(resourceAmount); 
        }

        public async Task<Crystal> GetCrystal(HtmlDocument document)
        {
            var resourceAmount = await ParseResource(ResourceType.Crystal, document);
            return new Crystal(resourceAmount);
        }

        public async Task<Deuterium> GetDeuterium(HtmlDocument document)
        {
            var resourceAmount = await ParseResource(ResourceType.Deuterium, document);
            return new Deuterium(resourceAmount);
        }
    }
}
