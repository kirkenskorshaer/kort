var express = require('express'),
  app = express(),
  port = process.env.PORT || 3000,
  mongoose = require('mongoose'),
  Member = require('./api/models/MemberModel'),
  Visit = require('./api/models/VisitModel'),
  VisitTimeAlert = require('./api/models/VisitTimeAlertModel'),
  Service = require('./api/models/ServiceModel'),
  bodyParser = require('body-parser');
  

mongoose.Promise = global.Promise;
mongoose.connect('mongodb://localhost/SpiseBilletten');


app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());


var memberRoutes = require('./api/routes/MemberRoutes');
memberRoutes(app);

var visitRoutes = require('./api/routes/VisitRoutes');
visitRoutes(app);

var visitTimeAlertRoutes = require('./api/routes/VisitTimeAlertRoutes');
visitTimeAlertRoutes(app);

var serviceRoutes = require('./api/routes/ServiceRoutes');
serviceRoutes(app);


app.use(function(req, res) {
  res.status(404).send({url: req.originalUrl + ' not found'})
});


app.listen(port);


console.log('SpiseBilletten server started on: ' + port);