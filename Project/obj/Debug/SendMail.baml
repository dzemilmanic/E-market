   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5   	       Project.SendMail   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Project     5   	     � Title$  SendMail��5   	   $	��470��6   $	��450��6'    � WindowStartupLocation$ CenterScreen=�5	   	   ���#899499.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ Vertical=�6"   .+�5      �� $(��"Unesite podatke i sadržaj e-maila��6   $��20(�6A   $��Center=�6O   $	��Top=�6l   $
��Bold"�5       �� Padding$ 10q�6   5      �� 5   
   $8��2Unesite e-mail korisnika kome želite poslati mail��6   $��1��6M   $��7q�6Z   $��Medium"�6e   �� 5   
   -   5         ��Name$ tbRecipientEmail��5      $��2��6,   $��10q�69   ���#D3D3D3$��Medium"�6Z   �� 5   
   $��Predmet e-maila:��6   $��3��6,   $��7q�69   $��Medium"�6D   �� 5   
   -   5        $ 	tbSubject��5      $��4��6%   $��10q�62   ���#D3D3D3$��Medium"�6S   �� 5!   
   $��Poruka e-maila:��6   $��5��6+   $��7q�68   $��Medium"�6C   �� 5"   
   -   5        $ tbBody��5"      $��6��6"   $��10q�6/   $	��150��6;   ���#D3D3D3$��Medium"�6]    �� TextWrapping$
 Wrap=�6q   �� 5$   
   -   5        $��Pošalji e-mail��5$      $��7��6*   $��10q�67      ButtonStyle#��[  6[   
 � Tag$	 250��6�   5%      5&      