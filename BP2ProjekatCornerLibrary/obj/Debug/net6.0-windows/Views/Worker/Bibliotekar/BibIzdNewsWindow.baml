   M S B A M L   `   `   `  ����S  OBP2ProjekatCornerLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullu?clr-namespace:BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar1BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar  5   	   H    BBP2ProjekatCornerLibrary.Views.Worker.Bibliotekar.BibIzdNewsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Qlocal?clr-namespace:BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar     5   	   $	��700��5      $
��1325��6%   .��5
      � .+�5      � -   5          �Name$  view_select��5   	   $y�	Collapsed=�6   .+�5      ��  �� Orientation$ Vertical=�6   .+�5      � -   5        $  
grd_TopBar��5      $	��100��5       � RowDefinitions 5      �� 5      $��95 �6   �� 5      $��5 �6   .+�5      � -   5        $  grd_TopBar_Content��5       � ColumnDefinitions 5      �� 5   	   $	��70* �6   �� 5   	   $	��30* �6   .+�5      �� $ 
Horizontal=�6   .+�5   	   �� -   5        $��lb_Title��5      $��40(�6   $
��Bold"�6-   $��Uredi izdanje:��6?   �� 5   	   -   5        $��lb_izd_name��5      $��40(�6"   5   	   �� 5      -   5         ��Name$ btn_Add_News��5      $��1��5      $��Right=�5      $��10q�5      $	��250��5       $��60��5!      $��22(�5"      $��Dodaj izdanje novina��5#      5$      5%      � 5&      
�������$��1��5'      5(      �� 5)      -   5         ��Name$ sp_ListaKnjiga��5)      $	��550��5*      .+�5+      �  K� ZIndex$ 1��6   $��1 0 1 0q�6   $��25��6.    5,      �� 5-   	   $��4* �6   $��80��6%   �� 5.   	   $��5* �6   $	��100��6%   �� 5/   	   $��5* �6   $	��110��6%   �� 50   	   $	��15* �6   $	��150��6&   �� 51   	   $��6* �6   $��80��6%   �� 52   	   $	��15* �6   $	��150��6&   �� 53   	   $��9* �6   $	��128��6%   .+�57      �� -   5        $ btn_sort_brIzd��57      $�� Br. izdanja��58   
   $
��Left=�59   	   $��0��5:   %   5;   	   �� 5<      -	   5         ��Name$ img_sort_brIzd��5<      $$��..\..\..\Images\arrow_down.png��5=   
    J� IsHitTestVisible$ False��62   $��0��5>      $��Right=�5?      $ 2��5@   
   �� 5B      -
   5        $ btn_sort_cena��5B      $
��Cena��5C   
   $��1��5D   %   5E   	   �� 5F      -   5        $ img_sort_cena��5F      $$��..\..\..\Images\arrow_down.png��5G   
   $ False��62   $��1��5H      $��Right=�5I      $ 2��5J   
   �� 5M      -   5        $ btn_sort_datIzd��5M      $��Dat. izdanja��5N   
   $��2��5O   %   5P   	   �� 5Q      -   5        $ img_sort_dat��5Q      $$��..\..\..\Images\arrow_down.png��5R   
   $ False��62   $��2��5S      $��Right=�5T      $ 2��5U   
   �� 5W      -   5        $ btn_sort_jezici��5W      $��Jezici��5X   
   $��3��5Y   
   5Z      �� 5[      -   5        $ img_sort_jezici��5[      $$��..\..\..\Images\arrow_down.png��5\      $ False��64   $��3��5]      $��Right=�5^      $ 2��5_      �� 5a      -   5        $ btn_sort_format��5a      $�� Format��5b   
   $��4��5c   
   $
��Left=�5d   !   5e      �� 5f      -   5        $ img_sort_format��5f      $$��..\..\..\Images\arrow_down.png��5g      $ False��64   $��4��5h      $��Right=�5i      $ 2��5j      �� 5l      -   5        $ btn_sort_izdKuce��5l      $��Izdavačke kuće��5m   
   $��5��5n   
   5o      �� 5p      -   5        $ img_sort_izdKuce��5p      $$��..\..\..\Images\arrow_down.png��5q      $ False��64   $��5��5r      $��Right=�5s      $ 2��5t      �� 5v      -   5        $ btn_sort_period��5v      $��Period izdavanja��5w      $��6��5x      5y      �� 5z      -   5        $ img_sort_period��5z      $$��..\..\..\Images\arrow_down.png��5{   	   $ False��61   $��Right=�5|      $��6��5}      $ 2��5~   	   5�      �� 5�      -   5        	 ��Name$	 	IzdNovina��5�      
�������$��Disabled=�5�      $��-3 -25 0 0 q�5�      
 �� View
 5�      �� 5�   	   .P�5�   
   ��  �� DisplayMemberBinding 6   �� *	DispBrIzd+ �� Width 6D   �� *ActualWidth+ �� ElementName$ btn_sort_brIzd���� 5�   
    6   �� *
DispCena+ 6C   �� *ActualWidth+$ btn_sort_cena���� 5�   
    6   �� *
DispDatIzd+ 6E   �� *ActualWidth+$ btn_sort_datIzd���� 5�   
    6   �� *
ListJezici+ 6E   �� *ActualWidth+$ btn_sort_jezici���� 5�   
    6   �� *
DispFormat+ 6E   �� *ActualWidth+$ btn_sort_format���� 5�   
    6   �� *ListIzdKuce+ 6G   �� *ActualWidth+$ btn_sort_izdKuce���� 5�   
    6   �� *
DispPeriod+ 6E   �� *ActualWidth+$ btn_sort_period��5�   
   5�      � 5�      $y�Hidden=�6    5�      �� 5�   	   ��6   �� *ActualWidth+$
 col1�� 5�      �� 5�   	   $��1* �6   .+�5�      � -   5        $  helperField��5�      $��0��6   6   5�      5�      5�      5�      � 5�      -   5        $  	view_edit��5�   	   $y�Visible=�6    � 	Resources 5�       	  brush �� Color��%,   &	        �� 5�      $ DarkGray��6$   5�      .+�5�      �� .+�5�      �� $ 
Horizontal=�6   .+�5�      �� -   5         ��Name$ lb_Add_Izdanje��5�      $��30(�6%   $��Dodaj izdanje:��63   �� 5�      -   5        $ lb_Edit_Izdanje��5�      $��30(�6&   $y�	Collapsed=�64   $��Izmeni izdanje:��6K   �� 5�      -   5        $ lb_Name_edit��5�      $��30(�6#   $y�Visible=�61   5�      �� 5�      $��1��6   $��0��6   #��[  6*   $��0 2 0 0q�6O   ._�5�      � $��20 10q�6   $	��540��6    5�      �� 5�   	   $��1* �6   .+�5�      �  5�   	   �� 5�   
   $��4* �6   �� 5�   
   $��7* �6    5�   	   �� 5�   
   $��1* �6   �� 5�   
   $��1* �6   �� 5�   
   $��1* �6   �� 5�   
   $��1* �6   .+�5�   	   �� $��Right=�6   $��Center=�5�   
   $��20(�5�   
   $��0��5�   
   $��Broj izdanja:��6   �� 5�   	   $��Right=�6   $��Center=�5�      $��20(�5�      $��1��5�      $��Datum izdanja:��6   �� 5�   	   $��Right=�6   $��Center=�5�      $��20(�5�      $��2��5�      $��Cena:��5�   
   �� 5�   	   -   5        $ 
lb_AddHere��5�      $y�	Collapsed=�6#   $��Right=�5�      $��Center=�5�      $��20(�5�      $��3��5�      $��Dodaj u ovu filijalu:��5�   
   �� 5�   	   -   5         ��Name$ tb_BrIzd��5�      $��Center=�5�   	   $��20(�5�   	   $��3q�5�   	   $��50��5�   
   $��1��5�   	   $��0��5�   	   �� 5�   	   $ 
Horizontal=�6   $��1��6-   $��1��6:   $��3q�6J   .+�5�   
   �� -   5        $ tb_Dan��5�      $��20(�5�      $��70��5�      $��50��5�      $��Center=�5�      �� 5�   
   $��20(�6   $��Center=�6   .��69   /6<   �� 5�   
   -   5        $ tb_Mesec��5�      $��20(�5�   
   $��70��5�   
   $��50��5�   
   $��Center=�5�      �� 5�   
   $��20(�6   $��Center=�6   .��69   /6<   �� 5�   
   -    5        $ 	tb_Godina��5�      $��20(�5�   
   $	��120��5�   
   $��50��5�   
   $��Center=�5�      5�   
   �� 5�   	   -!   5        $ tb_Cena��5�      $��Center=�5�      $��20(�5�      $��50��5�      $��3q�5�      $��1��5�      $��2��5�      �� 5�   	   -"   5        $ 
sp_AddHere��5�      $y�	Collapsed=�6(   $ 
Horizontal=�5�      $��1��5�      $��8��5�      .+�5�   
   �� -#   5         ��Name$ 
cb_AddHere��5�      $��3q�5�      $��Center=�5�      5�      �� 5�   
   -$   5        $ lb_Kolicina��5�      $��20(�5      ${�False��5     $��Center=�5     $��
Količina:��6-   �� 5  
   -%   5        $ tb_Kolicina��5     $
��3 10q�5  
   $��50��5     $	��120��5  
   $��20(�5  
   ${�False��5  
   $��Center=�5	     6.   �� 5
  
   -&   5        $��lb_AlreadyInStore��5
     $��12(�6)   
�������$��Center=�6I   .��6d   #!*Trenutan broj izdanja u filijali6�   5  
   5  	   � 5     $��1��6   $y�	Collapsed=�6    5  	   �� 5  
   $��4* �6   �� 5  
   $��7* �6    5  	   �� 5  
   $��1* �6   �� 5  
   $��1* �6   .+�5  	   �� $ Vertical=�6   $��0��6+   .+�5  
   �� $��20(�6   $��3q�5     $��Right=�5     $��Center=�5     $��Jezici:��6.   �� 5  
   -'   5        $ btn_Add_Jezik��5     $��50��5  
   $	��150��6   $��Right=�5  
   $��20(�5   
   $��Dodaj jezik��5!  
   $y�	Collapsed=�5"  
   5#  
   �� 5%  	   $ Vertical=�6   $��1��6+   .+�5&  
   �� $��20(�6   $��3q�5'     $��Right=�5(     $��Izdavačke kuće:��6.   �� 5)  
   -(   5        $ 
btn_Add_IK��5)     $��80��5*  
   $	��150��6   $��Right=�5+  
   $��20(�5,  
   $$��       Dodaj 
izdavačku kuću��5-  
   5.  
   �� 50  	   -)   5         ��Name$ 
lbx_Jezici��50      �� DisplayMemberPath$ NazivJezika��51  	   $��3q�52  	   $��17(�53  	    �� SelectionMode$ Multiple=�54  	   $��1��55  	   $��0��56  	   �� 58  	   -*   5        $ lbx_IzdKuce��58     $ Naziv��59  	   $��3q�5:  	   $��17(�5;  	   $ Multiple=�5<  	   $��1��5=  	   $��1��5>  	   5?  	   5A     5B     � 5C     -+   5        $  grd_Add_Btns��5C     $y�	Collapsed=�6!    5D     �� 5E     $��8* �6   �� 5F     $��6* �6   �� 5G     $��1* �6   �� 5H     $��6* �6   �� 5I     $��8* �6   .+�5L     �� -,   5        $ btn_Add_Confirm��5L     $	��5 0q�5M     $��45��5N     $��30(�5O     
������$��1��5Q     $��Dodaj��5R     5S     �� 5T     --   5        $ btn_Add_Cancel��5T     $	��5 0q�5U     $��45��5V     $��30(�5W     
�������$��3��5Y     $��Poništi��5Z     5[     5\     � 5]     -.   5        $  grd_Edit_Btns��5]      5^     �� 5_     $��6* �6   �� 5`     $��6* �6   �� 5a     $��1* �6   �� 5b     $��6* �6   �� 5c     $��1* �6   �� 5d     $��6* �6   �� 5e     $��6* �6   .+�5h     �� -/   5        $ btn_Edit_Cancel��5h     $	��5 0q�5i     $��45��5j     $��30(�5k     
�������$��1��5m     $��Poništi��5n     5o     �� 5p     -0   5        $ btn_Edit_Delete��5p     $	��5 0q�5q     $��45��5r     $��30(�5s     
�������$��3��5u     $��Obriši��5v     5w     �� 5x     -1   5        $ btn_Edit_Confirm��5x     $	��5 0q�5y     $��45��5z     $��30(�5{     
������$��5��5}     $��Sačuvaj��5~     5     5�     5�     5�     5�     5�     