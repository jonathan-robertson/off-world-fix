using OffWorldFix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OffWorldFix.Commands
{
    internal class ConsoleCmdOffWorldFix : ConsoleCmdAbstract
    {
        private static readonly ModLog<ConsoleCmdOffWorldFix> _log = new ModLog<ConsoleCmdOffWorldFix>();
        private static readonly string[] _commands = new string[] {
            "offworldfix",
            "owf"
        };
        private readonly string _help;

        public ConsoleCmdOffWorldFix()
        {
            var dict = new Dictionary<string, string>() {
                { "debug", "toggle debug logging mode" },
                //{ "purge <x> <z>", "remove a vertical column of land (including bedrock)" },
            };

            var i = 1; var j = 1;
            _help = $"Usage:\n  {string.Join("\n  ", dict.Keys.Select(command => $"{i++}. {GetCommands()[0]} {command}").ToList())}\nDescription Overview\n{string.Join("\n", dict.Values.Select(description => $"{j++}. {description}").ToList())}";
        }

        public override string[] getCommands()
        {
            return _commands;
        }

        public override string getDescription()
        {
            return "Configure or adjust settings for the Off World Fix mod.";
        }

        public override string getHelp()
        {
            return _help;
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
        {
            try
            {
                if (_params.Count == 0)
                {
                    SdtdConsole.Instance.Output($"At least 1 parameter is required; use '_help {_commands[0]}' to learn more.");
                    return;
                }

                switch (_params[0])
                {
                    case "debug":
                        ModApi.DebugMode = !ModApi.DebugMode;
                        SdtdConsole.Instance.Output($"Debug logging is now {(ModApi.DebugMode ? "enabled" : "disabled")}.");
                        _log.Info($"Debug logging has been {(ModApi.DebugMode ? "enabled" : "disabled")} by {SafelyGetSenderName(_senderInfo)}.");
                        return;
                        //case "purge":
                        //    if (_params.Count != 3) { break; }
                        //    if (!int.TryParse(_params[1], out var x)) { break; }
                        //    if (!int.TryParse(_params[2], out var z)) { break; }
                        //    HandlePurge(x, z);
                        //    return;
                }
                SdtdConsole.Instance.Output($"Invald request; use '_help {_commands[0]}' to learn more.");
            }
            catch (Exception e)
            {
                SdtdConsole.Instance.Output($"Exception encountered: \"{e.Message}\"\n{e.StackTrace}");
                _log.Error($"Exception encountered when {SafelyGetSenderName(_senderInfo)} tried to run 'offworldfix' console command.", e);
            }
        }

        private static string SafelyGetSenderName(CommandSenderInfo _senderInfo)
        {
            var clientInfo = _senderInfo.RemoteClientInfo;
            return clientInfo != null
                ? $"{clientInfo.playerName} ({clientInfo.entityId} // {clientInfo.CrossplatformId})"
                : "[[TELNET/CP]]";
        }

        //private static void HandlePurge(int x, int z)
        //{
        //    try
        //    {
        //        var _changes = new List<BlockChangeInfo>();
        //        for (var y = 0; y < 255; y++)
        //        {
        //            _changes.Add(new BlockChangeInfo(0, new Vector3i(x, y, z), BlockValue.Air));
        //        }
        //        SdtdConsole.Instance.Output($"number of blocks: {_changes.Count}");
        //        GameManager.Instance.SetBlocksRPC(_changes);
        //    }
        //    catch (Exception e)
        //    {
        //        SdtdConsole.Instance.Output($"Failed to delete the column of blocks at {x}, {z}.");
        //        _log.Error($"Failed to delete the column of blocks from {x}, {z}.", e);
        //    }
        //    SdtdConsole.Instance.Output($"Successfully deleted the column of blocks at {x}, {z}.");
        //    _log.Info($"Successfully deleted the column of blocks at {x}, {z}.");
        //}
    }
}
