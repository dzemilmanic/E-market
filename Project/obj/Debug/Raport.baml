   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5          Project.Raport   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Project     5        N� Title$  Raport��5	       N� 
Background
 ����� � DataContext 5
      ��  �� RelativeSource �� *Self+.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $	��250 �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6    �� Orientation$ Vertical=�6"   .+�5      �� $��25(�64   $��Center=�6B   $	��Top=�6_   $
��Bold"�5       �� Padding$ 10q�6   ��5      �� *
RaportText+5      5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��15q�6;   .+�5      �� $��15(�64   ��6   �� *
StatusText+64   �� 5      -   5         ��Name$ txtTotalSales��5      $��15(�6I   ��6-   �� *
TotalSales+6I   5      �� 5   
   -   5         ��Name$ lvOrderedProducts��5      $��2��6.   $��10q�6;   $
��auto��6G   	 �� View	 5      �� 5       .P�5!      �� $��	Ime Kupca��6%   
 �� Width$	
 120��6b    �� DisplayMemberBinding 68   �� *
imeKupca+6b   �� 5"      $��Proizvod��6%   $	
 150��6e    67   �� *imeProizvoda+6e   �� 5#      $��	Količina��6%   $	
 100��6a    67   �� *
Kolicina+6a   �� 5$      $
��Cena��6%   $	
 100��6Y    63   �� *Cena+6Y   �� 5%      $��Ime Prodavca��6%   $	
 150��6h    6;   �� *imeProdavca+6h   5&      5(      �� 5)   
   $��3��6   .+�5*      �� -   5        $��Generiši izveštaj��5*         ButtonStyle#��[  6K   
 � Tag$	 250��6p   5+      5,      5-      