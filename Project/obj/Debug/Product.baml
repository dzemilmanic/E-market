   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5          Project.Product   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Project     5        N� Title$  Product��5	       N� 
Background
 ����� � DataContext 5      ��  �� RelativeSource �� *Self+.��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   .+�5      �� $��Dodaj/ažuriraj proizvode��6   $��25(�68   $��Center=�6F   $	��Top=�6c   $
��Bold"�5       �� Padding$ 10q�6#   5      �� 5   
   $��1��6   $��5q�6"    �� Orientation$ 
Horizontal=�6-   .+�5      �� $��Naziv proizvoda:��6   $��Center=�60   $��5,0,5,0q�6K   �� 5      -   5         ��Name$ txtProductName��5      $��5,0,5,0q�6.   $	��100��6?   $��30��6K   �� 5      $��Cena proizvoda:��6   $��Center=�6/   $��5,0,5,0q�6J   �� 5      -   5        $ txtProductPrice��5      $��5,0,5,0q�6/   $��50��6@   $��30��6K   �� 5      $��Kolicina proizvoda:��6   $��Center=�63   $��5,0,5,0q�6N   �� 5      -   5        $ txtProductQuantity��5      $��50��62   $��5,0,5,0q�6=   $��30��6N   �� 5      $ Vertical=�6   $��10,0,0,0q�60   .+�5       �� -   5        $��Dodaj proizvod��5          ButtonStyle#��[  63   
 � Tag$	 100��6X   6b   �� 5!      -   5        	 ��Name$	 	btnUpdate��5!   ~   $��Ažuriraj proizvod��6   #��[  66   $	 100��6[   6~   5"      5#      �� 5%   
   -   5        
 ��Name$
 
lvProducts��5%      $��2��6'   $��5q�64    �� View 5&      �� 5'      .P�5(      �� $��Naziv��6%    �� Width$	 200��6[    �� DisplayMemberBinding 64   �� *Naziv+6[   �� 5)      $
��Cena��6%   $	 200��6Y    63   �� *Cena+6Y   �� 5*      $��	Količina��6%   $	 200��6a    67   �� *
Kolicina+6a   5+      5-      �� 5.   
   $��3��6   $��Right=�6"   $��5,5,20,5q�6>   .+�5/      �� -   5        $	 	btnDelete��5/   w   $��Obriši proizvod��6   #��[  6/   $	 100��6T   $��5,5,0,0q�6�   50      51      52      