   M S B A M L   `   `   `  ����S  OBP2ProjekatCornerLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullu?clr-namespace:BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar1BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar  5   	   F    @BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar.BibAutorWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Qlocal?clr-namespace:BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar     5   	   $	��700��5      $
��1325��6%   .��5
      � .+�5      � -   5          �Name$  view_select��5   	   $y�	Collapsed=�6   .+�5      ��  �� Orientation$ Vertical=�6   .+�5      � -   5        $  
grd_TopBar��5      $	��100��5       � RowDefinitions 5      �� 5      $��95 �6   �� 5      $��5 �6   .+�5      � -   5        $  grd_TopBar_Content��5       � ColumnDefinitions 5      �� 5   	   $	��50* �6   �� 5   	   $	��50* �6   .+�5      �� $��40(�6   $
��Bold"�6   $��Odabir autora:��6.   �� 5      -   5         ��Name$ btn_Add_Autor��5      $��1��5      $��Right=�5      $��10q�5      $	��150��5      $��60��5      $��22(�5      $��Dodaj autora��5       5!   	   5"      � 5#      
�������$��1��5$      5%      �� 5&      -   5         ��Name$ sp_ListaKnjiga��5&      $	��550��5'      .+�5(      �  K� ZIndex$ 1��6   $��1 0 1 0q�6   $��25��6.    5)      �� 5*   	   $��5* �6   $	��150��6%   �� 5+   	   $��5* �6   $	��150��6%   �� 5,   	   $��1* �6   $	��120��6%   �� 5-   	   $	��15* �6   $	��150��6&   �� 5.   	   $��5* �6   $	��180��6%   .+�51      �� -   5        $ btn_sort_ime��51      $	��Ime��52   
   $��0��53   %   54   	   �� 55      -   5         ��Name$ img_sort_ime��55      $$��..\..\..\Images\arrow_down.png��56   
    J� IsHitTestVisible$ False��62   $��0��57      $��Right=�58      $ 2��59   
   �� 5;      -   5        $ btn_sort_prezime��5;      $��Prezime��5<   
   $��1��5=   
   5>   	   �� 5?      -	   5        $ img_sort_prezime��5?      $$��..\..\..\Images\arrow_down.png��5@      $ False��64   $��1��5A      $��Right=�5B      $ 2��5C      �� 5E      -
   5        $ btn_sort_datRodj��5E      $��Dat. rođenja��5F   
   $��2��5G   
   5H   	   �� 5I      -   5        $ img_sort_datRodj��5I      $$��..\..\..\Images\arrow_down.png��5J      $ False��64   $��2��5K      $��Right=�5L      $ 2��5M      �� 5O      -   5        $ btn_sort_biografija��5O      $��
Biografija��5P   
   $��3��5Q   
   �� 5S      -   5        $ btn_sort_drzava��5S      $��Država porekla��5T      $��4��5U      5V   	   �� 5W      -   5        $ img_sort_drzava��5W      $$��..\..\..\Images\arrow_down.png��5X   	   $ False��61   $��Right=�5Y      $��4��5Z      $ 2��5[   	   5\      �� 5_      -   5        	 ��Name$	 Autori��5_      
�������$��Disabled=�5a      $��-3 -25 0 0 q�5b      ��5c   
   �� *ActualWidth+
 �� ElementName$
 sp_ListaKnjiga�� �� View 5f      �� 5g   	   .P�5h   
   ��  �� DisplayMemberBinding 6   �� *Ime+ �� Width 6>   �� *ActualWidth+$
 btn_sort_ime���� 5i   
    6   �� *
GetPrezime+ 6E   �� *ActualWidth+$
 btn_sort_prezime���� 5j   
    6   �� *DispDatRodj+ 6F   �� *ActualWidth+$
 btn_sort_datRodj���� 5k   
    6   �� *
Biografija+ 6F   �� *ActualWidth+$
 btn_sort_biografija���� 5l   
    6   �� *
DispDrzava+ 6E   �� *ActualWidth+$
 btn_sort_drzava��5m   
   5o      � 5r      $y�Hidden=�6    5s      �� 5u   	   ��6   �� *ActualWidth+$

 col1�� 5w      �� 5x   	   $��1* �6   .+�5{      � -   5        $  helperField��5{      $��0��6   6   5|      5~      5      5�      � 5�      -   5        $  	view_edit��5�   	   $y�Visible=�6    � 	Resources 5�       	  brush �� Color��%,   &	        �� 5�      $ DarkGray��6$   5�      .+�5�      �� .+�5�      �� -   5         ��Name$ lb_Add_Autor��5�      $��30(�6"   $��Dodaj autora:��60   �� 5�      -   5        $ lb_Edit_Autor��5�      $��30(�6#   $y�	Collapsed=�61   $��Izmeni autora:��6H   �� 5�      $��1��6   $��0��6   #��[  6*   $��0 2 0 0q�6O   ._�5�      � $��20 10q�6   $	��540��6    5�      �� 5�   	   $��1* �6   �� 5�   	   $��1* �6   .+�5�      �  5�   	   �� 5�   
   $��4* �6   �� 5�   
   $��7* �6    5�   	   �� 5�   
   $��1* �6   �� 5�   
   $��1* �6   �� 5�   
   $��1* �6   �� 5�   
   $��1* �6   .+�5�   	   �� $��Right=�6   $��Center=�5�   
   $��20(�5�   
   $��0��5�   
   $
��Ime:��6   �� 5�   	   $��Right=�6   $��Center=�5�      $��20(�5�      $��1��5�      $��Prezime:��6   �� 5�   	   $��Right=�6   $��Center=�5�      $��20(�5�      $��2��5�      $��Datum rođenja:��5�   
   �� 5�   	   $��Right=�6   $��Center=�5�      $��20(�5�      $��3��5�      $��Država porekla:��5�   
   �� 5�   	   -   5         ��Name$ tb_Ime��5�      $��Center=�5�   
   $��20(�5�   
   $��3q�5�   
   $��50��5�   
   $��1��5�   
   $��0��5�   
   �� 5�   	   -   5        $ 
tb_Prezime��5�      $��Center=�5�   
   $��20(�5�   
   $��3q�5�   
   $��50��5�   
   $��1��5�   
   $��1��5�   
   �� 5�   	   $ 
Horizontal=�6   $��2��6-   $��1��6:   $��3q�6J   .+�5�   
   �� -   5        $ tb_Dan��5�      $��20(�5�      $��70��5�      $��50��5�      $��Center=�5�      �� 5�   
   $��20(�6   $��Center=�6   .��69   /6<   �� 5�   
   -   5        $ tb_Mesec��5�      $��20(�5�      $��70��5�      $��50��5�      $��Center=�5�      �� 5�   
   $��20(�6   $��Center=�6   .��69   /6<   �� 5�   
   -   5        $ 	tb_Godina��5�      $��20(�5�      $	��120��5�      $��50��5�      $��Center=�5�      5�   
   �� 5�   	   -   5         ��Name$ 	cb_Drzava��5�      $��20(�5�   
   $��3q�5�   
   $��50��5�   
   $��3��5�   
   $��1��5�   
   $��Center=�5�       �� DisplayMemberPath$ NazivDrzave��5�      5�   	   � 5�      $��1��6    5�   	   �� 5�   
   $��2* �6   �� 5�   
   $��7* �6    5�   	   �� 5�   
   $��1* �6   .+�5�   
   �� $��20(�6   $��3q�5�      $��Right=�5�      $��Center=�5�      $��Biografija:��6.   �� 5�   	   -   5        $ tb_Biografija��5�      $��3q�5�   	   $��17(�5�   	   $��1��5�   	   $��1��5�   	    �� TextWrapping$
 Wrap=�5�      5�   	   5�      5�      � 5�      -   5        $  grd_Add_Btns��5�      $y�	Collapsed=�6!    5�      �� 5�      $��8* �6   �� 5�      $��6* �6   �� 5�      $��1* �6   �� 5�      $��6* �6   �� 5�      $��8* �6   .+�5�      �� -   5        $ btn_Add_Cancel��5�      $	��5 0q�5�      $��45��5�      $��30(�5�      
�������$��1��5�      $��Poništi��5      5     �� 5     -   5        $ btn_Add_Confirm��5     $	��5 0q�5     $��45��5     $��30(�5     
������$��3��5     $��Dodaj��5     5	     5
     � 5     -   5        $  grd_Edit_Btns��5      5     �� 5     $��6* �6   �� 5     $��6* �6   �� 5     $��1* �6   �� 5     $��6* �6   �� 5     $��1* �6   �� 5     $��6* �6   �� 5     $��6* �6   .+�5     �� -   5        $ btn_Edit_Cancel��5     $	��5 0q�5     $��45��5     $��30(�5     
�������$��1��5     $��Poništi��5     5     �� 5     -    5        $ btn_Edit_Delete��5     $	��5 0q�5     $��45��5      $��30(�5!     
�������$��3��5#     $��Obriši��5$     5%     �� 5&     -!   5        $ btn_Edit_Confirm��5&     $	��5 0q�5'     $��45��5(     $��30(�5)     
������$��5��5+     $��Sačuvaj��5,     5-     5.     5/     50     51     52     