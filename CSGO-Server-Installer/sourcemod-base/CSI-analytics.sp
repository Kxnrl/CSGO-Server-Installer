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

enum Function
{
    fncServer_Start = 0,
    fncMap_Start,
    fncPlayer_Join,
    fncPlayer_Leave,
    TotalFunction
};

static char g_szFunction[TotalFunction][32] = 
{
    "Server_Start",
    "Map_Start",
    "Player_Join",
    "Player_Leave"
};

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
    FormatEx(url, 192, "%s?action=server&host=%s&port=%d", REMOTE_SERVER, g_szServerIP, hostport.IntValue);
    System2HTTPRequest hRequest = new System2HTTPRequest(System2Callback, url);
    hRequest.Timeout = 60;
    hRequest.Any = fncStart_Start,
    hRequest.GET();
    delete hRequest;
}

public void OnMapStart()
{
    char map[128];
    GetCurrentMap(map, 128);

    char url[256];
    FormatEx(url, 256, "%s?action=map&host=%s&port=%d&player=%d&maxplayer=%d&map=%s", REMOTE_SERVER, g_szServerIP, hostport.IntValue, GetClientCount(false), MaxClients, map);
    System2HTTPRequest hRequest = new System2HTTPRequest(System2Callback, url);
    hRequest.Timeout = 60;
    hRequest.Any = fncMap_Start,
    hRequest.GET();
    delete hRequest;
}

public void OnClientPutInServer(int client)
{
    if(IsFakeClient(client) || IsClientSourceTV(client))
        return;

    char auth[32];
    if(!GetClientAuthId(client, AuthId_SteamID64, auth, 32, true))
        return;

    char url[256];
    FormatEx(url, 256, "%s?action=join&host=%s&port=%d&player=%d&maxplayer=%d&client=%s", REMOTE_SERVER, g_szServerIP, hostport.IntValue, GetClientCount(false), MaxClients, auth);
    System2HTTPRequest hRequest = new System2HTTPRequest(System2Callback, url);
    hRequest.Timeout = 60;
    hRequest.Any = fncPlayer_Join,
    hRequest.GET();
    delete hRequest;
}

public void OnClientDisconnect(int client)
{
    if(!IsClientInGame(client) || IsFakeClient(client) || IsClientSourceTV(client))
        return;

    char auth[32];
    if(!GetClientAuthId(client, AuthId_SteamID64, auth, 32, true))
        return;
    
    char url[256];
    FormatEx(url, 256, "%s?action=leave&host=%s&port=%d&player=%d&maxplayer=%d&client=%s", REMOTE_SERVER, g_szServerIP, hostport.IntValue, GetClientCount(false), MaxClients, auth);
    System2HTTPRequest hRequest = new System2HTTPRequest(System2Callback, url);
    hRequest.Timeout = 60;
    hRequest.Any = fncPlayer_Leave,
    hRequest.GET();
    delete hRequest;
}

public void System2Callback(bool success, const char[] error, System2HTTPRequest request, System2HTTPResponse response, HTTPRequestMethod method)
{
    if(!success)
    {
        char url[192];
        request.GetURL(url, 192);
        LogError("System2 -> %s -> [%s] -> %s", g_szFunction[request.Any], error, url);
        return;
    }

    if(response.StatusCode != 200)
    {
        char url[192];
        response.GetLastURL(url, 192);
        LogError("System2 -> %s -> HttpCode: %d -> %s -> %s", g_szFunction[request.Any], response.StatusCode, error, url);
        return;
    }
}
