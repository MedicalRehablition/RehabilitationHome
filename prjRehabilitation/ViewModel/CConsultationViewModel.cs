﻿using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel
{
    public class CConsultationViewModel
    {
        private Consultation _consult;
        public CConsultationViewModel()
        {
            _consult = new Consultation();
        }
        public Consultation Consult
        {
            get { return _consult; }
            set { _consult = value; }
        }
        public int FConsultId
        {
            get { return _consult.FConsultId; }
            set { _consult.FConsultId = value; }
        }        
        public int? PatinetId
        {
            get { return _consult.PatinetId; }
            set { _consult.PatinetId = value; }
        }
        public string? Date
        {
            get { return _consult.Date; }
            set { _consult.Date = value; }
        }
        public string? Summary
        {
            get { return _consult.Summary; }
            set { _consult.Summary = value; }
        }
        public string? Assessment
        {
            get { return _consult.Assessment; }
            set { _consult.Assessment = value; }
        }
        public string? Result
        {
            get { return _consult.Result; }
            set { _consult.Result = value; }
        }
    }
}