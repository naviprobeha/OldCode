   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      '    !StarPRNTSDK.CombinationSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        N� Title$  CombinationSamplePage��5	       � 	Resources 5      5   /StarPRNTSDK.GoNextPageWithSetReceiptTypeCommand #  GoCombinationExtPageTextCommand   ReceiptType#   StarPRNTSDK.GoNextPageCommand   NavigationUri   StarPRNTSDK.BaseCommand   
Executable ' #GoCombinationExtPageTextUTF8Command % !GoCombinationExtPageRasterCommand . *GoCombinationExtPageRasterBothScaleCommand * &GoCombinationExtPageRasterScaleCommand + 'GoCombinationExtPageRasterCouponCommand 5 1GoCombinationExtPageRasterCouponRotation90Command��%�  &	        &	 S     &	 �     &	 �     &	 ]    &	 �    &	       5   
   $
 Text=�6\   $# CombinationExtSamplePage.xaml?�6o    . 6�     5   
   $ TextUTF8=�6`   $# CombinationExtSamplePage.xaml?�6w    . 6�     5   
   $ Raster=�6^   $# CombinationExtSamplePage.xaml?�6s    . 6�     5   
   $ RasterBothScale=�6g   $# CombinationExtSamplePage.xaml?�6�    . 6�     5   
   $ RasterScale=�6c   $# CombinationExtSamplePage.xaml?�6}    . 6�     5   
   $ RasterCoupon=�6d   $# CombinationExtSamplePage.xaml?�6    . 6�     5   
   $ RasterCouponRotation90=�6n   $# CombinationExtSamplePage.xaml?�6�    . 6�   5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   ��  �� Orientation$ Vertical=�6   .+�5      ��   PageTitleStyle#��[ 6q   ��6   ��   ResourceManager �� Source# [  �� Path$  CombinationSamplePageTitle�6q   �� 5       	 ListBoxSeparatorStyle#��[	 6r   ��6   �� # [ $! StarIOExtManagerSampleTitle�6r   �� 5       
 ListBoxItemTemplate#��[
 6     ListBoxStyle#��[ 6a     ListBoxItemStyle#��[ 6�   ��6J   Z� .@�5      !   StarPRNTSDK.BaseListBoxItem     ForeGroundColor
 ��  �	   BorderBlushColor
	 ����� 
   ListBoxItemTouchedCommand#
 [  5          Title 5      ��   TextReceipt# [ $ Description�5         5"      
 ��  �
	 �����#
 [ 5&       5#      ��   TextReceiptUTF8# [ $ Description�5&        5(      
 ��  �
	 �����#
 [ 5,       5)      ��   RasterReceipt# [ $ Description�5,        5.      
 ��  �
	 �����#
 [ 52       5/      ��   RasterReceiptBothScale# [ $ Description�52        54      
 ��  �
	 �����#
 [ 58       55      ��   RasterReceiptScale# [ $ Description�58        5:      
 ��  �
	 �����#
 [ 5>       5;      ��   RasterCoupon# [ $ Description�5>        5@      
 ��  �#
 [ 5C       5A      ��   RasterCouponRotation90# [ $ Description�5C      5D      5F      5G      5H      