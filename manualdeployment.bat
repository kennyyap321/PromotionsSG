docker build -f PromotionsSG.API.Claim/Dockerfile -t claimapi .
docker build -f PromotionsSG.API.CustomerProfile/Dockerfile -t customerprofileapi .
docker build -f PromotionsSG.API.Event/Dockerfile -t eventapi .
docker build -f PromotionsSG.API.Feedback/Dockerfile -t feedbackapi .
docker build -f PromotionsSG.API.Follow/Dockerfile -t followapi .
docker build -f PromotionsSG.API.Login/Dockerfile -t loginapi .
docker build -f PromotionsSG.API.Membership/Dockerfile -t membershipapi .
docker build -f PromotionsSG.API.Notification/Dockerfile -t notificationapi .
docker build -f PromotionsSG.API.Promotion/Dockerfile -t promotionapi .
docker build -f PromotionsSG.API.Recommendation/Dockerfile -t recommendationapi .
docker build -f PromotionsSG.API.Search/Dockerfile -t searchapi .
docker build -f PromotionsSG.API.ShopProfile/Dockerfile -t shopprofileapi .
docker build -f PromotionsSG.Presentation.WebPortal/Dockerfile -t webportalapi .


docker tag claimapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/claimapi:latest
docker tag customerprofileapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/customerprofileapi:latest
docker tag eventapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/eventapi:latest
docker tag feedbackapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/feedbackapi:latest
docker tag followapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/followapi:latest
docker tag loginapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/loginapi:latest
docker tag membershipapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/membershipapi:latest
docker tag notificationapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/notificationapi:latest
docker tag promotionapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/promotionapi:latest
docker tag recommendationapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/recommendationapi:latest
docker tag searchapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/searchapi:latest
docker tag shopprofileapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/shopprofileapi:latest
docker tag webportalapi:latest 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/webportalapi:latest


docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/claimapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/customerprofileapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/eventapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/feedbackapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/followapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/loginapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/membershipapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/notificationapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/promotionapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/recommendationapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/searchapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/shopprofileapi:latest
docker push 702668669288.dkr.ecr.ap-southeast-1.amazonaws.com/webportalapi:latest

pause