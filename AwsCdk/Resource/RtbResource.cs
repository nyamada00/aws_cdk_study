using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace AwsCdk.Resource
{
    internal class RtbResource
    {
        internal CfnRouteTable? PublicRtb { get; private set; }
        internal CfnRouteTable? App1aRtb { get; private set; }
        internal CfnRouteTable? App1cRtb { get; private set; }
        internal CfnRouteTable? DbRtb { get; private set; }

        private record RouteInfo(string Id, string DestinationCidrBlock,
        System.Func<string>? GatewayId, System.Func<string>? NatGatewayId);
        private record AssociationInfo(string Id, System.Func<string> SubnetId);

        private record ResourceInfo(string Id, RouteInfo[] Routes, AssociationInfo[] Associations, string ResourceName, System.Action<CfnRouteTable> Assign);

        private readonly CfnVPC? vpc;
        private readonly CfnInternetGateway? igw;
        private readonly CfnSubnet? subnetPublic1a;
        private readonly CfnSubnet? subnetPublic1c;
        private readonly CfnSubnet? subnetApp1a;
        private readonly CfnSubnet? subnetApp1c;
        private readonly CfnSubnet? subnetDb1a;
        private readonly CfnSubnet? subnetDb1c;
        private readonly CfnNatGateway? natGateway1a;
        private readonly CfnNatGateway? natGateway1c;

        private RtbResource() { }

        /// <summary>
        ///  コンストラクタ
        /// </summary>
        /// <param name="vpc"></param>
        /// <param name="subnetPublic1a"></param>
        /// <param name="subnetPublic1c"></param>
        /// <param name="subnetApp1a"></param>
        /// <param name="subnetApp1c"></param>
        /// <param name="subnetDb1a"></param>
        /// <param name="subnetDb1c"></param>
        /// <param name="igw"></param>
        /// <param name="natGateway1a"></param>
        /// <param name="natGateway1c"></param>
        /// <returns></returns>
        public RtbResource(
         Construct scope,
         CfnVPC vpc,
         CfnSubnet subnetPublic1a,
         CfnSubnet subnetPublic1c,
         CfnSubnet subnetApp1a,
         CfnSubnet subnetApp1c,
         CfnSubnet subnetDb1a,
         CfnSubnet subnetDb1c,
         CfnInternetGateway igw,
         CfnNatGateway natGateway1a,
         CfnNatGateway natGateway1c
         ) : base()
        {
            this.vpc = vpc;
            this.subnetPublic1a = subnetPublic1a;
            this.subnetPublic1c = subnetPublic1c;
            this.subnetApp1a = subnetApp1a;
            this.subnetApp1c = subnetApp1c;
            this.subnetDb1a = subnetDb1a;
            this.subnetDb1c = subnetDb1c;
            this.igw = igw;
            this.natGateway1a = natGateway1a;
            this.natGateway1c = natGateway1c;

            var resourcesInfos = new[]{
                new ResourceInfo(
                    Id: "RouteTablePublic",
                    Routes: new[]{
                        new RouteInfo(
                            Id: "RoutePublic",
                            DestinationCidrBlock: "0.0.0.0/0",
                            GatewayId: () => this.igw.Ref,
                            NatGatewayId: null
                        )
                    },
                    Associations: new []{
                        new AssociationInfo(
                            Id: "AssociationPublic1a",
                            SubnetId: () => this.subnetPublic1a.Ref
                        ),
                         new AssociationInfo(
                            Id: "AssociationPublic1c",
                            SubnetId: () => this.subnetPublic1c.Ref
                        ),
                    },
                    ResourceName: "rtb-public",
                    Assign: rtb => this.PublicRtb = rtb
                ),
                new ResourceInfo(
                    Id: "RouteTableApp1a",
                    Routes: new[]{
                        new RouteInfo(
                            Id: "RouteApp1a",
                            DestinationCidrBlock: "0.0.0.0/0",
                            GatewayId: null,
                            NatGatewayId: ()=>this.natGateway1a.Ref
                        )
                    },
                    Associations: new []{
                        new AssociationInfo(
                            Id: "AssociationApp1a",
                            SubnetId: () => this.subnetApp1a.Ref
                        ),
                    },
                    ResourceName: "rtb-app-1a",
                    Assign: rtb => this.App1aRtb = rtb
                ),
                new ResourceInfo(
                    Id: "RouteTableApp1c",
                    Routes: new[]{
                        new RouteInfo(
                            Id: "RouteApp1c",
                            DestinationCidrBlock: "0.0.0.0/0",
                            GatewayId: null,
                            NatGatewayId: ()=>this.natGateway1c.Ref
                        )
                    },
                    Associations: new []{
                        new AssociationInfo(
                            Id: "AssociationApp1c",
                            SubnetId: () => this.subnetApp1c.Ref
                        ),
                    },
                    ResourceName: "rtb-app-1c",
                    Assign: rtb => this.App1cRtb = rtb
                ),
                new ResourceInfo(
                    Id: "RouteTableDb",
                    Routes: System.Array.Empty<RouteInfo>(),
                    Associations: new []{
                        new AssociationInfo(
                            Id: "AssociationDb1a",
                            SubnetId: () => this.subnetDb1a.Ref
                        ),
                        new AssociationInfo(
                            Id: "AssociationDb1c",
                            SubnetId: () => this.subnetDb1c.Ref
                        ),
                    },
                    ResourceName: "rtb-db",
                    Assign: rtb => this.DbRtb = rtb
                ),
            };

            CreateResources(scope, resourcesInfos);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourceInfoList"></param>
        private void CreateResources(Construct scope, ResourceInfo[] resourceInfoList)
        {
            foreach (var resourceInfo in resourceInfoList)
            {
                var subnet = CreateRouteTable(scope, resourceInfo);
                resourceInfo.Assign(subnet);
            }
        }

        /// <summary>
        /// ルートテーブル作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourceInfo"></param>
        /// <returns></returns>
        private CfnRouteTable CreateRouteTable(Construct scope, ResourceInfo resourceInfo)
        {
            var routeTable = new CfnRouteTable(scope, resourceInfo.Id,
                new CfnRouteTableProps
                {
                    VpcId = this.vpc!.Ref,
                    Tags = new CfnTag[]{
                        new CfnTag{
                            Key= "Name",
                            Value = ResourceUtility.CreateResourceName(scope, resourceInfo.ResourceName)
                        }
                    }
                });
            foreach (var routeInfo in resourceInfo.Routes)
            {
                CreateRoute(scope, routeInfo, routeTable);
            }

            foreach (var associationInfo in resourceInfo.Associations)
            {
                CreateAssociation(scope, associationInfo, routeTable);
            }
            return routeTable;
        }

        /// <summary>
        /// Association作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="associationInfo"></param>
        /// <param name="routeTable"></param>
        private static void CreateAssociation(Construct scope, AssociationInfo associationInfo, CfnRouteTable routeTable)
        {
            _ = new CfnSubnetRouteTableAssociation(scope, associationInfo.Id, new CfnSubnetRouteTableAssociationProps
            {
                RouteTableId = routeTable.Ref,
                SubnetId = associationInfo.SubnetId()
            });
        }

        /// <summary>
        /// Route作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="routeInfo"></param>
        /// <param name="routeTable"></param>
        private static void CreateRoute(Construct scope, RouteInfo routeInfo, CfnRouteTable routeTable)
        {
            var route = new CfnRoute(scope, routeInfo.Id, new CfnRouteProps
            {
                RouteTableId = routeTable.Ref,
                DestinationCidrBlock = routeInfo.DestinationCidrBlock
            });

            if (routeInfo.GatewayId != null)
            {
                route.GatewayId = routeInfo.GatewayId();
            }
            else if (routeInfo.NatGatewayId != null)
            {
                route.NatGatewayId = routeInfo.NatGatewayId();
            }
        }
    }
}
