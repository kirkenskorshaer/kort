'use strict';
var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var VisitTimeAlertSchema = new Schema(
{
  HoursFromLatestVisitBeforeAlert:
  {
    type: Number
  },
  CreatedDate:
  {
    type: Date,
    default: Date.now
  },
  MemberId:
  {
    type: Schema.Types.ObjectId
  }
});

module.exports = mongoose.model('VisitTimeAlert', VisitTimeAlertSchema);