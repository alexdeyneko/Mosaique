   M S B A M L   `   `   `  ����D  @wpfMozaiq, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:wpfMozaiq.VeiwwpfMozaiq.Veiw  5      9!clr-namespace:wpfMozaiq.ViewModelwpfMozaiq.ViewModel  5
      #    wpfMozaiq.Veiw.EditMozaicView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      R NMahApps.Metro.IconPacks, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5      (localclr-namespace:wpfMozaiq.Veiw  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      1	viewModel!clr-namespace:wpfMozaiq.ViewModel  5
        � Title$;  5Добавление мозайки в каталог��5       � WindowStartupLocation$ CenterScreen=�5      $	��454��5      $	��226��5       � DataContext 5      -   'wpfMozaiq.ViewModel.EditMozaicViewModel  5   
   .��5      �� $
��auto��6   $��0,0,0,0q�6   .+�5   
   �� $	��100��5      $��70��5      $��-285,20,0,0q�5      ��5      �� *FilenameImage+�� 5   
   $��75��5      $��170,-80,116,0q�5      $��Имя файла :��5      �� 5!   
   -   5         ��Name$ FileName��5"      $	��202��5#      $��25��5$      $��150,-50,0,173q�5%      $
��Left=�5&         AccentColorBrush#���   5'      $��1q�5(      ��5)      �� *FilenameImage+�� 5*   
   $��60��5+      $��25��5,      $��0,-200,10,0q�5-      $��Right=�5.      $	��Top=�5/       J� RenderTransformOrigin$ -0.689,-4.943&�54      ��50      �� *ChoiseFileImage+��51      /  )MahApps.Metro.IconPacks.PackIconExtension  *�� *)'iconPacks:PackIconMaterialKind.FileFind++   Height$ 13X�   Width$ 13X�54      �� 55   
   $
��auto��56      $��16��57      $��80,-250,0,0q�58      $
��Left=�59      $��Имя мозайки:��5:      �� 5;   
   -   5        $ 	IndexName��5<      $	��202��5=      $��25��5>      $��210,-250,0,0q�5?      $
��Left=�5@      #���   5A      $��1q�5B      ��5C      �� *EditNameMozaic+�� 5E   
   $��
5,-145,5,5q�5F      $��Right=�5G       �� Orientation$ 
Horizontal=�5H      .+�5I      �� $��5q�5J      $��5q�5K      $��OK��5M       �� 	IsDefault . ��5L      �� *	OkCommand+5N      �� 5O      $��5,5,5,5q�5P      $��5q�5Q      $��Cancel��5S      	 �� IsCancel	 . ��5R      �� *CancelCommand+5T      5U      5X      5Z      