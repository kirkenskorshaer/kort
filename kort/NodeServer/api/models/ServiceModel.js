'use strict';
var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var ServiceSchema = new Schema(
{
	Name:
	{
		type: String
	},
	MaxServiceUses:
	{
		type: Number,
		get: value => Math.round(value),
		set: value => Math.round(value)
	},
	ServiceUses:
	[{
		UsedTime:
		{
			type: Date,
			default: Date.now
		}
	}],
	MemberId:
	{
		type: Schema.Types.ObjectId
	}
});

module.exports = mongoose.model('Service', ServiceSchema);