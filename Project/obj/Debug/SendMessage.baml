   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5          Project.SendMessage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Project     5        N� Title$  SendMessage��5	       N� 
Background
 ����� � DataContext 5      ��  �� RelativeSource �� *Self+.��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $	��100 �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ Vertical=�6"   .+�5      �� $��Pošaljite poruku��6   $��25(�60   $��Center=�6>   $	��Top=�6[   $
��Bold"�5       �� Padding$ 10q�6   5      �� 5   
   $��1��6   $��10,0,0,0q�6"   .+�5      �� $��Unesite tekst poruke:��6   5      �� 5   
   $��2��6   $ 
Horizontal=�6"   $��
7, 0, 0, 5q�6;   .+�5      �� -   5         ��Name$ tbMessageContent��5      $	��400��60   $��5q�6<    �� TextWrapping$
 Wrap=�6G   	 �� AcceptsReturn	 . 6[   �� 5      -   5        
 ��Name$
 cbRecipients��5      $	��100��6-   $��5q�69   $��30��6D    �� DisplayMemberPath$ Username��6P    �� SelectedValuePath$ 
KorisnikID��6m   $��Center=�5      $��Medium"�6;   $��15(�6O   �� 5       $ Vertical=�6   .+�5!      �� -   5        $��Pošalji poruku��5!      $��	20,15,0,0q�62      ButtonStyle#��[  6b   
 � Tag$	 100��6�   �� 5"      -   5        $��Pošalji email��5"      $��	20,15,0,0q�61   #��[  6_   $	 100��6�   5#      5$      �� 5%   
   $��3��6   $��10,0,0,0q�6"   .+�5&      �� $��Vaše poruke:��6   5'      �� 5)   
   -   5         ��Name$ 
lvMessages��5)      $��4��6'   $��7q�6S   ��64   �� *Poruke+ �� View 5*      �� 5+      .P�5,      �� $��
Posiljalac��6%    �� Width$	 100��6h    �� DisplayMemberBinding 69   �� *PosiljalacIme+6h   �� 5-      $��Primalac��6%   $	 100��6d    67   �� *PrimalacIme+6d   �� 5.      $��Sadržaj��6%   $	 300��6_    66   �� *	Sadrzaj+6_   �� 5/      $��Datum slanja��6%   $	 150��6h    6;   �� *DatumSlanja+6h   50      52      53      54      