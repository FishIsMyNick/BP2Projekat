﻿#pragma checksum "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E08857AB62AE2F67B5193155BE9F233BED6B3831"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BP2ProjekatCornerLibrary.Views.Worker;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BP2ProjekatCornerLibrary.Views.Worker {
    
    
    /// <summary>
    /// BibAddMagazinWindow
    /// </summary>
    public partial class BibAddMagazinWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbID;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGenerisi;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNaziv;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPeriod;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCena;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDodaj;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BP2ProjekatCornerLibrary;component/views/worker/bibaddmagazinwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tbID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btnGenerisi = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
            this.btnGenerisi.Click += new System.Windows.RoutedEventHandler(this.btnGenerisi_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbNaziv = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cbPeriod = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.tbCena = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnDodaj = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\..\..\Views\Worker\BibAddMagazinWindow.xaml"
            this.btnDodaj.Click += new System.Windows.RoutedEventHandler(this.btnDodaj_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

