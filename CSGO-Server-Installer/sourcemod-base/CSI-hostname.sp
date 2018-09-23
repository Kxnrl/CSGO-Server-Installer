#pragma semicolon 1
#pragma newdecls required

public Plugin myinfo = 
{
    name        = "[CSI] - Base Plugin -> Hostname",
    author      = "Kyle",
    description = "Set server hostname",
    version     = "1.0",
    url         = "https://kxnrl.com"
};

static ConVar hostname;
static ConVar sv_tags;

public void OnPluginStart()
{
    hostname = FindConVar("hostname");
    if(hostname == null) SetFailState("Failed to find convar 'hostname'.");

    sv_tags = FindConVar("sv_tags");
    if(sv_tags == null) SetFailState("Failed to find convar 'sv_tags'.");
}

public void OnConfigsExecuted()
{
    SetTag();

    char path[128];
    BuildPath(Path_SM, path, 128, "configs/hostname.cfg");
    File file = OpenFile(path, "r");
    if(file == null)
    {
        hostname.SetString("[CSI] 您尚未设置服务器名称.", false, false);
        return;
    }

    char buffer[128];
    file.ReadString(buffer, 128, -1);
    TrimString(buffer);

    hostname.SetString(buffer, false, false);

    file.Close();
}

static void SetTag()
{
    char buffer[128];
    sv_tags.GetString(buffer, 128);

    if(StrContains(buffer, "kxnrl.com") == -1)
    {
        StrCat(buffer, 128, ",kxnrl.com");
        StrCat(buffer, 128, ",CSI");
        sv_tags.SetString(buffer, false, false);
    }
}
