﻿using MoneyManager.ViewModel;
using MoneyManager_BL_DAL;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MoneyManager.Views.Dialogs.Update
{
    public sealed partial class UpdateDebt : ContentDialog
    {
        public DebtViewModel DebtViewModel { get; set; }

        public CategoryViewModel CategoryViewModel { get; set; }

        public ObservableCollection<Category> categories = Category.RetrieveAll();

        public ObservableCollection<MType> type = MType.RetrieveAll();

        public UpdateDebt()
        {
            this.InitializeComponent();
            DebtViewModel = new DebtViewModel();
        }

        public UpdateDebt(DebtViewModel updvm)
        {
            this.InitializeComponent();
            DebtViewModel = updvm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int year = date.Date.Year, month = date.Date.Month, day = date.Date.Day, hour = time.Time.Hours, min = time.Time.Minutes, sec = time.Time.Seconds;
            DebtViewModel.debt.deadline = new DateTime(year, month, day, hour, min, sec);
            DebtViewModel.Update();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            long selectedValue = (long)cmb.SelectedValue;
            DebtViewModel.debt.category_id = selectedValue;
        }
    }
}
