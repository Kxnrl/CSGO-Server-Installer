#pragma semicolon 1
#pragma newdecls required

#include <system2>

public Plugin myinfo = 
{
    name        = "[CSI] - Base Plugin -> Analytics",
    author      = "Kyle",
    description = "Set server hostname",
    version     = "1.0",
    url         = "https://kxnrl.com"
};

// 本插件仅作为大数据统计使用.
// 如果不需要查看您的服务器相关数据.
// 请把本插件移动到disabled文件夹.

#define REMOTE_SERVER "https://api.kxnrl.com/CSI/v1/"

static char g_szServerIP[24];

ConVar hostport;

public void OnPluginStart()
{
    ConVar hostip = FindConVar("hostip");
    if(hostip == null)
        SetFailState("hostip is invalid CVar");

    hostport = FindConVar("hostport");
    if(hostport == null)
        SetFailState("hostport is invalid CVar");

    FormatEx(g_szServerIP, 24, "%d.%d.%d.%d", ((hostip.IntValue & 0xFF000000) >> 24) & 0xFF, ((hostip.IntValue & 0x00FF0000) >> 16) & 0xFF, ((hostip.IntValue & 0x0000FF00) >>  8) & 0xFF, ((hostip.IntValue & 0x000000FF) >>  0) & 0xFF);

    char url[192];
    FormatEx(url, 192, "%s?action=start&server=%s&port=%d", REMOTE_SERVER, g_szServerIP, hostport.IntValue);
    System2HTTPRequest hRequest = new System2HTTPRequest(Server_Start, url);
    hRequest.Timeout = 60;
    hRequest.GET();
    delete hRequest;
}

public void OnMapStart()
{
    char map[128];
    GetCurrentMap(map, 128);

    char url[256];
    FormatEx(url, 256, "%s?action=map&server=%s&port=%d&player=%d&maxplayer=%d", REMOTE_SERVER, g_szServerIP, hostport.IntValue, GetClientCount(false), MaxClients);
    System2HTTPRequest hRequest = new System2HTTPRequest(Map_Start, url);
    hRequest.Timeout = 60;
    hRequest.GET();
    delete hRequest;
}

public void Server_Start(bool success, const char[] error, System2HTTPRequest request, System2HTTPResponse response, HTTPRequestMethod method)
{
    if(!success)
    {
        char url[192];
        request.GetURL(url, 192);
        LogError("System2 -> Server_Start -> [%s] -> %s", error, url);
        return;
    }

    if(response.StatusCode != 200)
    {
        char url[192];
        response.GetLastURL(url, 192);
        LogError("System2 -> Server_Start -> HttpCode: %d -> %s -> %s", response.StatusCode, error, url);
        return;
    }
}

public void Map_Start(bool success, const char[] error, System2HTTPRequest request, System2HTTPResponse response, HTTPRequestMethod method)
{
    if(!success)
    {
        char url[192];
        request.GetURL(url, 192);
        LogError("System2 -> Map_Start -> [%s] -> %s", error, url);
        return;
    }

    if(response.StatusCode != 200)
    {
        char url[192];
        response.GetLastURL(url, 192);
        LogError("System2 -> Map_Start -> HttpCode: %d -> %s -> %s", response.StatusCode, error, url);
        return;
    }
}
