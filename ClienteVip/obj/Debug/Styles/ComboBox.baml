   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxToggleButton  �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels �Name � Points �� Triggers	 P� Property
 P� Value �� 
TargetName " ButtonMouseOverBackgroundBrush " ButtonIsPressedBackgroundBrush  DisabledForegroundBrush  ControlBaseStyle �� BasedOn  DefaultForegroundBrush  DefaultBackgroundBrush  DefaultBorderBrush �Name � ColumnDefinitions �Name � AllowsTransparency �� Mode �� RelativeSource ��Name �� ClearTypeHint ��Name ��Name �� ElementName ��Name-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation d�Name d� 	IsChecked ��Name �� SelectionBoxItemTemplate �� SelectionBoxItem# �� SelectionBoxItemStringFormat  �� ContentStringFormat! J� IsHitTestVisible" ]� 
Conditions# �� Property$ �� Value% P� 
SourceName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�  &	        7  7  7  '�� J    7  7  7  7  7   7  7  7  �� 5      "  d�6'   .�5      ��  � �� .  6"   �� 5       � |� .  6"   �� 5       �@��	ClickMode$ Press=�6"   �� 5       � �� 5      �� 5	   
   " d�6   .R�5
      �� #��z��6   #��z��5   !   #��z��5   !   ._�5      �� $ InnerBorder��6   
������ $��Right=�5   !   $��0q�6=    . #��� r�6c   ._�5      � $ arrow��6    �    z  	= @KL #��z��5   '   $��Center=�5   '   $��Center=�5   '   5      5       5      P� 5      	 �@J�IsMouseOver
 . . �5      �� $ InnerBorder��6-   8    � ��6F   5      P� 5      	 � ��
 . . �5      �� $ InnerBorder��6-   8   � ��6F   5      P� 5      	 � {�
 .  . �5      �� $ arrow��6'   8   � ��6:   5      5!      5#      5$      �� 5&      "  ��6   8   .�5'      ��  � ��8  6#   �� 5(       � ��8  6#   �� 5)       � ��8  6$   �� 5*       � ��$ 1q�6(   �� 5+       � ��$
 Auto=�6C   �� 5,       � ��$
 Auto=�6A   �� 5-       � ��$ 6,3,5,3q�6    �� 5.       � �� . 66   �� 5/       �@��PanningMode$
 Both=�61   �� 50       � �� 51      �� 52   
   " ��6   .R�53      � $ templateRoot��6    .  54      �� 55      $��* �6!   �� 56      #��� r�6!   $��0 �6t   .+�58      � $ 
PART_Popup��6    . $��2��6B   $��1q�6�   #��� .�6�   $��Bottom=�6  ��6V   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.��59      �� $ dropDownBorder��6   #��� ��6/   $��1q�6{   #��� ��6�   ._�5:      �� $ DropDownScrollViewer��6   .��5;      � $
 grid��6   $ Enabled=�6'   .+�5<      �� $ canvas��6   $
��Left=�6-   $��0��6H   $	��Top=�6S   $��0��6k   .+�5=      �� $ 
opaqueRect��6"   ��66   �� *
Background+$ dropDownBorder����6n   �� *ActualHeight+$ dropDownBorder����6�   �� *ActualWidth+$ dropDownBorder��5>      �� 5?      $ ItemsPresenter��6%   $ 	Contained=�6=   # z 6r   5@      5A      5B      5C      d� 5D      $ toggleButton��6   #��z��61   #��z��5E      #��z��5F      #��z��5G      $��2��5H      8��  5I      �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+5J      �� 5K      $ contentPresenter��6   #��z 69   #��z��5L   #   #��z 5M   #   #  z 5N   #   #��z��5O   #   ! .  #��z��5P   <   # z 5Q   #   #��z��6_   5R       5S      P� 5T      	 �@��HasItems
 .  . �5U      �� $ dropDownBorder��6)    � ��$ 95��6E   5V      P� 5W      	 � {�
 .  . �5X      ��  � �� �#1010106-   5Y      ]� 5Z      " 5[      �� 5\      # �@��
IsGrouping$ . 62   �� 5]      "# �@%�IsVirtualizingWhenGrouping$ .  6T   ./�5_      ��  � �� .  6@   5`      P� 5a      $% DropDownScrollViewer��6?   	 � ��
 .  . �5b      �� $ 
opaqueRect��6-    6E   �� *VerticalOffset+$ DropDownScrollViewer�� �@��Top�� 5c      $ 
opaqueRect��6.    6F   �� *HorizontalOffset+$ DropDownScrollViewer�� �@��Left5d      5f      5h      �� 5i       � �� 5j      �� 5k   
   "  ��6   .�5l      ��  � ��8  6)   �� 5m       � ��8  6)   �� 5n       � ��8  6*   5o      5q      5r      5s      