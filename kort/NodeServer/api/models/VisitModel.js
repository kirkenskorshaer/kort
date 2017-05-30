'use strict';
var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var VisitSchema = new Schema(
{
	VisitTime:
	{
		type: Date,
		default: Date.now
	},
	MemberId:
	{
		type: Schema.Types.ObjectId
	}
});

module.exports = mongoose.model('Visit', VisitSchema);