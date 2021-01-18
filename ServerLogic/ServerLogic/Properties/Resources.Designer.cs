﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServerLogic.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ServerLogic.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: clear [--help]
        ///
        ///DESCRIPTON
        ///
        ///TODO
        ///
        ///OPTIONS
        ///
        ///--help
        ///  Shows this..
        /// </summary>
        internal static string ClearHelpMessage {
            get {
                return ResourceManager.GetString("ClearHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to v0.0.1.
        /// </summary>
        internal static string CurrentVersion {
            get {
                return ResourceManager.GetString("CurrentVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: exit [--help]
        ///
        ///DESCRIPTON
        ///
        ///Exits the application and thus terminates the ServerLogic and
        ///closes the shell.
        ///
        ///OPTIONS
        ///
        ///--help
        ///  Shows this..
        /// </summary>
        internal static string ExitHelpMessage {
            get {
                return ResourceManager.GetString("ExitHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: qq [--version] [--help] &lt;password | password port&gt;
        ///
        ///These are commands that can be used in various situations:
        ///
        ///   password   Show or set the server-access password
        ///   port       Show or set the port
        ///   start      Start the server
        ///   stop       Stop the server
        ///   log        Show the logs
        ///   clear      Clear the logs
        ///   exit       Exit the shell
        ///
        ///Use &apos;&lt;command&gt; --help&apos; to read about a specific command..
        /// </summary>
        internal static string HelpHelpMessage {
            get {
                return ResourceManager.GetString("HelpHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please make sure to set a password that is at least 8 characters in length,
        ///but no more than 32, and satisfies 3 out of the following 4 rules:
        ///  At least one digit
        ///  At least one lowercase character
        ///  At least one uppercase character
        ///  At least one special character.
        /// </summary>
        internal static string InvalidPasswordExceptionMessage {
            get {
                return ResourceManager.GetString("InvalidPasswordExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please make sure the port is an integer between 1024 and 65535..
        /// </summary>
        internal static string InvalidPortExceptionMessage {
            get {
                return ResourceManager.GetString("InvalidPortExceptionMessage", resourceCulture);
            }
        }

        internal static string LogFilePath {
            get
            {
                return ResourceManager.GetString("LogFilePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: log [--help]
        ///
        ///DESCRIPTON
        ///
        ///TODO
        ///
        ///OPTIONS
        ///
        ///--help
        ///  Shows this..
        /// </summary>
        internal static string LogHelpMessage {
            get {
                return ResourceManager.GetString("LogHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: password [--help] &lt;string&gt;
        ///
        ///DESCRIPTON
        ///
        ///Shows the password that is currently set and will be used to
        ///connect to the ServerLogic. Alternatively it can also set a
        ///new password.
        ///
        ///If the command is of form &apos;password&apos;, the currently set password
        ///will be shown.
        ///If the command is of form &apos;password string&apos;, a new password
        ///will be set according to the string provided.
        ///
        ///OPTIONS
        ///
        ///&lt;string&gt;
        ///   The string that will be set as the new password used to
        ///   access the ServerLogic through the Moderator- [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string PasswordHelpMessage {
            get {
                return ResourceManager.GetString("PasswordHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: port [--help] &lt;integer&gt;
        ///
        ///DESCRIPTON
        ///
        ///Shows the port that is currently set and will be used to set
        ///up the WebSocket of the ServerLogic. Alternatively it can also
        ///set a new port.
        ///
        ///If the command is of form &apos;port&apos;, the currently set port will
        ///be shown.
        ///If the command is of form &apos;port integer&apos;, a new port will be
        ///set according to the integer provided.
        ///
        ///OPTIONS
        ///
        ///&lt;integer&gt;
        ///   The integer that will be set as the new port of the WebSocket
        ///   of the ServerLogic.
        ///
        ///--help
        ///   Shows this..
        /// </summary>
        internal static string PortHelpMessage {
            get {
                return ResourceManager.GetString("PortHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: start [--help] [--password=&lt;string&gt;] [--port=&lt;integer&gt;]
        ///
        ///DESCRIPTON
        ///
        ///Starts the ServerLogic with the given password and port
        ///Alternatively one can also pass options to start the change with
        ///a different password and/or a different port.
        ///
        ///If the command is of form &apos;start&apos;, the server will be set up
        ///and started with the currently set password and port.
        ///
        ///OPTIONS
        ///
        ///--help
        ///   Shows this.
        ///
        ///--password=&lt;string&gt;
        ///   Sets a new password used to access the ServerLogic through
        ///
        ///--port=&lt;integer&gt;
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StartHelpMessage {
            get {
                return ResourceManager.GetString("StartHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: stop [--help]
        ///
        ///DESCRIPTON
        ///
        ///Stops the ServerLogic.
        ///
        ///OPTIONS
        ///
        ///--help
        ///   Shows this..
        /// </summary>
        internal static string StopHelpMessage {
            get {
                return ResourceManager.GetString("StopHelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to usage: version [--help]
        ///
        ///DESCRIPTON
        ///
        ///Shows the current version of the ServerLogic.
        ///
        ///OPTIONS
        ///
        ///--help
        ///  Shows this..
        /// </summary>
        internal static string VersionHelpMessage {
            get {
                return ResourceManager.GetString("VersionHelpMessage", resourceCulture);
            }
        }
    }
}
