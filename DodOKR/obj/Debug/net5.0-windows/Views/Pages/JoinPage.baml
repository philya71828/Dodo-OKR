   M S B A M L   `   `   `  ����A  =DodOKR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DodOKRDodOKR  5          DodOKR.JoinPage   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:DodOKR     5      $	��695��5	      $
��1280��6     N� Title$  JoinPage��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��Center=�5      $��Center=�62   $��0,0,0,47q�6M   .+�5      �� $��Images/DodoLogo.png��6   $	��300��5      $	��175��6    �� 5      $��
Почта��6   $��14(�6%   $��Segoe UI)�63   $��5,5,5,5q�6I   �� 5         TextBoxStyle#��[  6   $��Segoe UI Light)�6<   $��5,5,5,5q�6X   $��14(�6j   �� 5       J� Opacity$ 0Q�6   $��10��6$   �� 5      $��Пароль��6   $��14(�6&   $��Segoe UI)�64   $��5,5,5,5q�6J   G� 5        PasswordBoxStyle#��[ 6   $��Segoe UI Light)�6D   $��0,0,0,0q�6`   $��5,5,5,5q�6q   $��14(�6�   �� 5      $ 0Q�6   $��30��6$   �� 5      $ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5         � Cursor$
 Hand��5      $	��140��64   $��Stretch=�6@   $��Stretch=�6^   $��0,0,5,0q�6z   ��5      �� 5      .R�5      �� $��30��6%    �� CornerRadius$ 5��61   ��5   "   �� 5    &    �� 
TargetType" ��6,   .�5!   *   ��  �� Property � �� �� Value �	#FFFF69006G   	 �� Triggers	 5"   *   P� 5#   .   
 P� Property
 �@J�IsMouseOver P� Value$
 True��6M   . �5$   2   ��  � ��
 � ���6O   5%   /   5'   '   ._�5)   "   �� $��
Войти��6,     
ButtonText#��[ 69   $��Segoe UI Semibold)�6]   $
��Bold"�6|   5*      5+      5-      �� 5.      -   5        $
 Hand��5.   8   $	��140��6F   $��5,0,0,0q�6R   ��5/      �� 50      .R�51      �� $ 5��6%   $��30��66   
����������	#FFFF6900$��2q�6m   ��52   "   �� 53   &   " ��6,   .�54   *   ��  � ��
 �����6G   	 55   *   P� 56   .   
 �@J�IsMouseOver$
 True��6M   . �57   2   ��  � ��
 � ���6O   58   /   5:   '   ._�5<   "   �� $��Регистрация��6,   #��[ 6?   ���	#FFFF6900$��Segoe UI Semibold)�6z   5=      5>      5@      5A      �� 5B      $��Normal$�6   $��Light"�6(   $��0,0,0,0q�6;   $
��True��6M   $��5,5,5,5q�6r   ��5C      �� 5D      .R�5E      �� ._�5F      �� $ ��Забыли пароль?��6(   
���   �$��14(�5H   (   $��Center=�5I   (   $��	Underline{�5J   (   $
 Hand��5K   (   5L      5M      5O      5P          DodOKR.RegistrationControl  5Q   
   -   5          Name$ panel��5Q   $   $y�Hidden=�63   $��0,0,0,0q�6G   5R      5S      