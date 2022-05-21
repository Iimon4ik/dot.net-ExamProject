namespace SushiShop.Services;

internal interface IFileService
{ 
    Task WriteLog(string message);
}