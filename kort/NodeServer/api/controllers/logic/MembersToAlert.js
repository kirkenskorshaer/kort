'use strict';


var mongoose = require('mongoose'),
	Member = mongoose.model('Member'),
	VisitTimeAlert = mongoose.model('VisitTimeAlert'),
	Visit = mongoose.model('Visit');


exports.getMembersToAlert = function(req, res)
{
	VisitTimeAlert.find({},function(err, visitTimeAlerts)
	{
		if (err)
		{
			res.send(err);
		}
		
		//console.log('alert = ' + visitTimeAlerts)

		//var alertObjects = JSON.parse(visitTimeAlerts);

		var members = [];
		visitTimeAlerts.forEach(function(item, index)
		{
			//var currentMember = addMemberIfRaised(item, members);
			//addMemberIfRaised(item, members);
			/*
			console.log(currentMember);
			if(currentMember)
			{
				members.push(currentMember);
			}
			*/
		});

		for (var currentAlertIndex = visitTimeAlerts.length - 1; currentAlertIndex >= 0; currentAlertIndex--)
		{
			var currentAlert = visitTimeAlerts[currentAlertIndex];
			addMemberIfRaised(currentAlert, members);
		}


		res.json(members);
	});
}

function addMemberIfRaised(alert, members)
{
	var visitsAfterThisDatePreventsAlert = new Date();
	visitsAfterThisDatePreventsAlert.setHours(visitsAfterThisDatePreventsAlert.getHours() - alert.HoursFromLatestVisitBeforeAlert);

	var memberId = alert.MemberId;
	var memberToReturn;

	Visit.find({MemberId:memberId, VisitTime : {$gt : visitsAfterThisDatePreventsAlert}},function(err, visits)
	{
		if(visits.length == 0)
		{
			console.log('no visits');
			Member.find({_id:memberId},function(err, member)
			{
				//console.log(member);
				//memberToReturn = member;
				members.push(member);
				console.log(members);
			});
		}
		else
		{
			console.log('visit memberid = ' + memberId + ' visits first id = ' + visits[0]._id);
		}
	}).limit(1);
	
	//console.log(memberToReturn);
	//return memberToReturn
}


/*
function foreachAlert(item, index)
{
	console.log('Alert' + index + ' - ' + item);
}
*/